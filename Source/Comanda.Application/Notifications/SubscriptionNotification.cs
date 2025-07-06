namespace Comanda.Application.Notifications;

public sealed record SubscriptionNotification : INotification
{
    public SubscriptionCreatedEventData Subscription { get; init; } = default!;
}