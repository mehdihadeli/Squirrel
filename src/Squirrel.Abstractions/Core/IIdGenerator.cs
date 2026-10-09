namespace Squirrel.Abstractions.Core;

public interface IIdGenerator<out TId>
{
    TId New();
}
