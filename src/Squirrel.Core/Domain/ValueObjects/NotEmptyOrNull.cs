using Squirrel.Core.Extensions;

namespace Squirrel.Core.Domain.ValueObjects;

public record NotEmptyOrNull
{
    protected NotEmptyOrNull(string value) => Value = value.NotBeEmptyOrNull();

    public string Value { get; }
}
