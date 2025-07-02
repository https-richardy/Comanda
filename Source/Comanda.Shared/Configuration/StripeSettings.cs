namespace Comanda.Shared.Configuration;

public sealed class StripeSettings
{
    public string SecretKey { get; set; } = default!;
    public string PlanId { get; set; } = default!;
    public string WebhookSecret { get; set; } = default!;
}