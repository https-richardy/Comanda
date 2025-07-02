namespace Comanda.Shared.Configuration;

public sealed class Settings : ISettings
{
    public MongoSettings Mongo { get; set; } = default!;
    public KeycloakSettings Keycloak { get; set; } = default!;
    public StripeSettings Stripe { get; set; } = default!;
}