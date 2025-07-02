namespace Comanda.Application.Payloads;

public sealed record SubscribePlanRequest : IRequest<Result<SubscriptionRedirect>>;