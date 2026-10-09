using Squirrel.Abstractions.Core;

namespace Squirrel.Core.IdsGenerator;

public class GuidIdGenerator : IIdGenerator<Guid>
{
    public Guid New()
    {
        return Guid.NewGuid();
    }
}
