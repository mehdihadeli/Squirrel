using Marten.Schema.Identity;

namespace Squirrel.Persistence.Marten;

public static class MartenIdGenerator
{
    public static Guid New() => CombGuidIdGeneration.NewGuid();
}
