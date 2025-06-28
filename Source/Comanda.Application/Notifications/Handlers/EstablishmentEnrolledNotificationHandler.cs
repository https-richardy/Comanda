namespace Comanda.Application.Notifications.Handlers;

public sealed class EstablishmentEnrolledNotificationHandler(
    IEstablishmentOwnerRepository ownerRepository,
    IEstablishmentRepository establishmentRepository
) : INotificationHandler<UserEnrolledNotification>
{
    public async Task Handle(UserEnrolledNotification notification, CancellationToken cancellationToken)
    {
        var email = notification.Credentials.Email;
        var name = notification.Credentials.Name;

        var owner = new EstablishmentOwner
        {
            Email = email,
            Name = name
        };

        var subscription = new Subscription { Status = SubscriptionStatus.None };
        var establishment = new Establishment
        {
            Name = "Meu Estabelecimento",
            Owner = owner,
            Subscription = subscription,
        };

        await establishmentRepository.SaveAsync(establishment);
        await ownerRepository.SaveAsync(owner);
    }
}