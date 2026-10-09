using Squirrel.Core.Events.Internal;
using Squirrel.Core.Types;
using Squirrel.Core.Types;
using FluentAssertions;

namespace Squirrel.Core.UnitTests;

public class TypeMapperTests
{
    [Fact]
    public void get_type_name_should_return_correct_name()
    {
        TypeMapper.AddFullTypeName(typeof(OrderCreated)).Should().Be(typeof(OrderCreated).FullName);
        TypeMapper.AddShortTypeName(typeof(OrderCreated)).Should().Be(nameof(OrderCreated));
        TypeMapper.GetAllTypeNames(typeof(OrderCreated)).Should().Contain(typeof(OrderCreated).FullName!);
    }

    [Fact]
    public void get_type_should_return_correct_type()
    {
        TypeMapper.GetType(nameof(OrderCreated)).Should().Be<OrderCreated>();
    }
}

public record OrderCreated : DomainEvent;
