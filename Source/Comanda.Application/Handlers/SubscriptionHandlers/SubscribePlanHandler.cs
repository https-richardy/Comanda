namespace Comanda.Application.Handlers;

public sealed class SubscribePlanHandler :
    IRequestHandler<SubscribePlanRequest, Result<SubscriptionRedirect>>
{
    public async Task<Result<SubscriptionRedirect>> Handle(SubscribePlanRequest request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(Result<SubscriptionRedirect>.Failure(OwnerErrors.NotFound));
    }
}