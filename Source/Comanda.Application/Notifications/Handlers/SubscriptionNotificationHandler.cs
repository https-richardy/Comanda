namespace Comanda.Application.Notifications.Handlers;

public sealed class SubscriptionNotificationHandler(
    IEstablishmentRepository establishmentRepository,
    IEstablishmentOwnerRepository ownerRepository
) : INotificationHandler<SubscriptionNotification>
{
    public async Task Handle(SubscriptionNotification notification, CancellationToken cancellationToken)
    {
        var subscription = notification.Subscription;

        var ownerEmail = subscription.Metadata.FirstOrDefault(selector => selector.Key == "email").Value;
        var ownerFilters = new EstablishmentOwnerFilters.Builder()
            .WithEmail(ownerEmail)
            .Build();

        var owners = await ownerRepository.GetOwnersAsync(ownerFilters);
        var owner = owners.FirstOrDefault()!;

        var establishmentFilters = new EstablishmentFilters.Builder()
            .WithOwnerId(owner.Id)
            .Build();

        var establishments = await establishmentRepository.GetEstablishmentsAsync(establishmentFilters);
        var establishment = establishments.FirstOrDefault()!;

        establishment.Subscription.Status = SubscriptionStatus.Active;
        establishment.Subscription.SubscriptionId = subscription.SubscriptionId;

        await establishmentRepository.UpdateAsync(establishment);
    }
}