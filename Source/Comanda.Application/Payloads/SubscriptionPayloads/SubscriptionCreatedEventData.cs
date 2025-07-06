namespace Comanda.Application.Payloads;

public sealed record SubscriptionCreatedEventData
{
    public string SubscriptionId { get; init; } = default!;
    public Dictionary<string, string> Metadata { get; init; } = [];
}