namespace Squirrel.Integration.Wolverine.InMemory;

public sealed class WolverineInMemoryBusOptions
{
    public bool EnableDurability { get; set; }
    public bool UseDurableLocalQueues { get; set; } = true;
    public bool UseEntityFrameworkCoreTransactions { get; set; } = true;
    public string? DurabilityConnectionString { get; set; }
}