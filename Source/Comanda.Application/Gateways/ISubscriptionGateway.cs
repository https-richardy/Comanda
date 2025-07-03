namespace Comanda.Application.Gateways;

public interface ISubscriptionGateway
{
    Task<Result<SubscriptionRedirect>> CreateSubscriptionSessionAsync(User user);
}