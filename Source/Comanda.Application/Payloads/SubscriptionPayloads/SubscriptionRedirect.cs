namespace Comanda.Application.Payloads;

public sealed record SubscriptionRedirect
{
    public string Url { get; init; } = default!;
}