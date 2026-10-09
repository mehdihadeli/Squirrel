using Squirrel.Abstractions;

namespace Squirrel.Abstractions.UnitTests;

public class FilterModelTests
{
    [Fact]
    public void constructor_should_set_filter_model_properties()
    {
        var filter = new FilterModel("Name", "Equals", "Squirrel");

        filter.FieldName.ShouldBe("Name");
        filter.Comparision.ShouldBe("Equals");
        filter.FieldValue.ShouldBe("Squirrel");
    }
}
