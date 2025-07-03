namespace Comanda.Application.Handlers;

public sealed class SubscribePlanHandler(
    ISubscriptionGateway subscriptionGateway,
    IAuthenticatedUserProvider userProvider
):  IRequestHandler<SubscribePlanRequest, Result<SubscriptionRedirect>>
{
    public async Task<Result<SubscriptionRedirect>> Handle(
        SubscribePlanRequest request,
        CancellationToken token
    )
    {
        var user = await userProvider.GetUserAsync();
        var result = await subscriptionGateway.SubscribePlanAsync(user);

        return Result<SubscriptionRedirect>.Success(result.Data!);
    }
}