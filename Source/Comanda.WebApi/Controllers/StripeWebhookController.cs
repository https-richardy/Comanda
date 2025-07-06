namespace Comanda.WebApi.Controllers;

[ApiController]
[Route("api/v1/webhooks")]
public sealed class StripeWebhookController(IMediator mediator, ISettings settings) : ControllerBase
{
    [HttpPost("stripe")]
    public async Task<IActionResult> OnNotificationAsync([FromHeader(Name = "Stripe-Signature")] string signature)
    {
        using (var reader = new StreamReader(Request.Body))
        {
            var payload = await reader.ReadToEndAsync();
            var webhookSecret = settings.Stripe.WebhookSecret;
            var stripeEvent = EventUtility.ConstructEvent(payload, signature, webhookSecret);

            if (stripeEvent.Type == EventTypes.CheckoutSessionCompleted)
            {
                var session = stripeEvent.Data.Object as Session;
                var metadata = session!.Metadata?.ToDictionary(selector => selector.Key, selector => selector.Value) ?? [];

                var subscriptionEvent = new SubscriptionCreatedEventData
                {
                    SubscriptionId = session!.SubscriptionId,
                    Metadata = metadata
                };

                var notification = new SubscriptionNotification
                {
                    Subscription = subscriptionEvent
                };

                await mediator.Publish(notification, CancellationToken.None);
            }
        }

        return Ok(new { message = "Stripe webhook received." });
    }
}