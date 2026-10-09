using Squirrel.Core.Extensions;

namespace Squirrel.Core.Domain.ValueObjects;

public record NotNull<T>
{
    protected NotNull(T value) => Value = value.NotBeNull();

    public T Value { get; }
}
