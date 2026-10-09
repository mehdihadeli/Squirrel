import { defineConfig } from "vitepress";

export default defineConfig({
  title: "Squirrel",
  description: "Reusable .NET building blocks for cloud-native applications.",
  base: process.env.GITHUB_ACTIONS
    ? `/${process.env.GITHUB_REPOSITORY?.split("/")[1] ?? "Squirrel"}/`
    : "/",
  cleanUrls: true,
  appearance: true,
  lastUpdated: true,
  themeConfig: {
    nav: [
      { text: "Guide", link: "/guide/quickstart" },
      { text: "Architecture", link: "/guide/architecture" },
      { text: "Packages", link: "/reference/packages" },
    ],
    sidebar: {
      "/guide/": [
        {
          text: "Start here",
          items: [
            { text: "Quickstart", link: "/guide/quickstart" },
            { text: "Architecture", link: "/guide/architecture" },
            { text: "Development", link: "/guide/development" },
            { text: "Versioning and releases", link: "/guide/versioning" },
          ],
        },
      ],
      "/reference/": [
        {
          text: "Reference",
          items: [
            { text: "Packages", link: "/reference/packages" },
            {
              text: "Components",
              items: [
                { text: "Abstractions", link: "/components/abstractions" },
                { text: "Core", link: "/components/core" },
                { text: "Web", link: "/components/web" },
                { text: "Security", link: "/components/security" },
                { text: "Health checks", link: "/components/health-check" },
                { text: "Resiliency", link: "/components/resiliency" },
                { text: "Email", link: "/components/email" },
                { text: "Validation", link: "/components/validation" },
                { text: "Caching", link: "/components/caching" },
                {
                  text: "Azure Redis caching",
                  link: "/components/caching-azure-redis",
                },
                { text: "Serialization", link: "/components/serialization" },
                { text: "Wolverine", link: "/components/wolverine" },
                {
                  text: "EF Core PostgreSQL",
                  link: "/components/persistence-efcore-postgres",
                },
                {
                  text: "Azure PostgreSQL",
                  link: "/components/persistence-efcore-azure-postgres",
                },
                {
                  text: "Azure Cosmos DB",
                  link: "/components/persistence-efcore-azure-cosmosdb",
                },
                { text: "MongoDB", link: "/components/persistence-mongo" },
                { text: "Marten", link: "/components/persistence-marten" },
                {
                  text: "EventStoreDB",
                  link: "/components/persistence-eventstoredb",
                },
                { text: "OpenTelemetry", link: "/components/opentelemetry" },
                { text: "OpenAPI", link: "/components/openapi" },
                {
                  text: "Serilog logging",
                  link: "/components/serilog-logging",
                },
                {
                  text: "Aspire integrations",
                  link: "/components/aspire-integrations",
                },
              ],
            },
          ],
        },
      ],
    },
    outline: "deep",
    search: { provider: "local" },
    footer: {
      message: "Reusable building blocks for cloud-native .NET applications.",
      copyright: "Squirrel contributors",
    },
  },
});
