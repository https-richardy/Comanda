namespace Comanda.Application.Notifications.Handlers;

public sealed class UserEnrolledNotificationHandler(IMediator mediator) :
    INotificationHandler<UserEnrolledNotification>
{
    public async Task Handle(UserEnrolledNotification notification, CancellationToken cancellationToken)
    {
        var task = notification.Credentials.Type switch
        {
            AccountType.Customer =>
                mediator.Publish(new CustomerRegisteredNotification(notification.Credentials)),

            AccountType.EstablishmentOwner =>
                mediator.Publish(new EstablishmentOwnerRegisteredNotification(notification.Credentials)),

            _ => Task.CompletedTask
        };

        await task;
    }
}