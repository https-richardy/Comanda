namespace Comanda.Domain.Entities;

public sealed class Subscription : Entity
{
    public SubscriptionStatus Status { get; set; }
    public string? PlanId { get; set; } = default!;
}