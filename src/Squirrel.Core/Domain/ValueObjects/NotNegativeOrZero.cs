using Squirrel.Core.Extensions;

namespace Squirrel.Core.Domain.ValueObjects;

public record NotNegativeOrZero
{
    public NotNegativeOrZero(int value) => Value = value.NotBeNegativeOrZero();

    public int Value { get; }
}
