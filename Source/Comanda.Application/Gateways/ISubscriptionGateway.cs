namespace Comanda.Application.Gateways;

public interface ISubscriptionGateway
{
    Task<Result<SubscriptionRedirect>> SubscribePlanAsync(User user);
}