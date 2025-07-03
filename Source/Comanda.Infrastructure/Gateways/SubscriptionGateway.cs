namespace Comanda.Infrastructure.Gateways;

public sealed class SubscriptionGateway(ISettings settings) : ISubscriptionGateway
{
    public async Task<Result<SubscriptionRedirect>> CreateSubscriptionSessionAsync(User user)
    {
        var price = settings.Stripe.PlanId;
        var subscription = new SessionLineItemOptions
        {
            Price = price,
            Quantity = 1,
        };

        var metadata = new Dictionary<string, string>
        {
            { "userId", user.Id.ToString() },
            { "email", user.Email },
        };

        var options = new SessionCreateOptions
        {
            Mode = "subscription",
            Metadata = metadata,
            LineItems = [subscription],
            SuccessUrl = settings.Client.CheckoutSuccessUrl,
            CancelUrl = settings.Client.CheckoutCancelUrl,
            SubscriptionData = new SessionSubscriptionDataOptions
            {
                TrialPeriodDays = 15,
                Metadata = metadata,
            },
        };

        var service = new SessionService();
        var session = await service.CreateAsync(options);

        return Result<SubscriptionRedirect>.Success(new SubscriptionRedirect { Url = session.Url });
    }
}