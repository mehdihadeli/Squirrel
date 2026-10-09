#!/usr/bin/env bash

set -euo pipefail

usage() {
  cat <<'EOF'
Usage:
  ./release-version.sh prepare-train <major.minor.patch>
  ./release-version.sh prepare-rc <major.minor.patch>
  ./release-version.sh prepare-stable <major.minor.patch>
  ./release-version.sh tag

Examples:
  ./release-version.sh prepare-train 1.1.0
  ./release-version.sh prepare-rc 1.0.0
  ./release-version.sh prepare-stable 1.0.0
  ./release-version.sh tag
EOF
}

if [[ $# -lt 1 ]]; then
  usage
  exit 1
fi

if ! command -v dotnet >/dev/null 2>&1; then
  echo "dotnet is required. Restore repository tools with: dotnet tool restore" >&2
  exit 1
fi

clear_version_height_offset() {
  local temporary_file

  temporary_file="$(mktemp)"
  awk '
    /"versionHeightOffset":/ { next }
    /"versionHeightOffsetAppliesTo":/ { next }
    { print }
  ' version.json > "$temporary_file"
  mv "$temporary_file" version.json
}

prepare_train() {
  local base_version="$1"

  dotnet nbgv set-version "${base_version}-preview.{height}"
  clear_version_height_offset
  echo "Updated version.json to ${base_version}-preview.{height}. Open a pull request with this change."
}

prepare_rc() {
  local base_version="$1"

  dotnet nbgv set-version "${base_version}-rc.{height}"
  echo "Updated version.json to ${base_version}-rc.{height}. Open a pull request with this change."
}

prepare_stable() {
  local base_version="$1"

  dotnet nbgv set-version "$base_version"
  clear_version_height_offset
  echo "Updated version.json to ${base_version}. Open a pull request with this change."
}


case "$1" in
  prepare-train)
    [[ $# -eq 2 ]] || { usage; exit 1; }
    prepare_train "$2"
    ;;
  prepare-rc)
    [[ $# -eq 2 ]] || { usage; exit 1; }
    prepare_rc "$2"
    ;;
  prepare-stable)
    [[ $# -eq 2 ]] || { usage; exit 1; }
    prepare_stable "$2"
    ;;
  tag)
    dotnet nbgv tag
    ;;
  *)
    usage
    exit 1
    ;;
esac