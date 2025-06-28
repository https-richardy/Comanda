namespace Comanda.Application.Notifications.Handlers;

public sealed class CustomerRegisteredNotificationHandler(ICustomerRepository customerRepository) :
    INotificationHandler<CustomerRegisteredNotification>
{
    public async Task Handle(CustomerRegisteredNotification notification, CancellationToken cancellationToken)
    {
        var customer = new Customer
        {
            Name = notification.Credentials.Name,
            Email = notification.Credentials.Email,
        };

        await customerRepository.SaveAsync(customer);
    }
}