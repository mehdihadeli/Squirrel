using Squirrel.Core.Extensions;

namespace Squirrel.Core.Domain.ValueObjects;

public record NotEmptyGuid
{
    protected NotEmptyGuid(Guid value) => Value = value.NotBeEmpty();

    public Guid Value { get; }
}
