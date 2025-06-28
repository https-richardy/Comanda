namespace Comanda.Application.Payloads;

public sealed record OwnerDetails
{
    public bool HasEstablishment { get; init; }
    public bool HasSubscription { get; init; }

    public string? OwnerName { get; init; }
    public string? OwnerEmail { get; init; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public SubscriptionStatus SubscriptionStatus { get; init; }
}