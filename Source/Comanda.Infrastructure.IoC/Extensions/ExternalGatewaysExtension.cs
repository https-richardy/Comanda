namespace Comanda.Infrastructure.IoC.Extensions;

[ExcludeFromCodeCoverage]
public static class ExternalGatewaysExtension
{
    public static void AddExternalGateways(this IServiceCollection services, ISettings settings)
    {
        services.AddScoped<IAuthenticationGateway, KeycloakAuthenticationGateway>();
        services.AddHttpClient<IAuthenticationGateway, KeycloakAuthenticationGateway>(client =>
        {
            client.BaseAddress = new Uri($"{settings.Keycloak.Url}/realms/{settings.Keycloak.Realm}/");
        });

        services.AddScoped<IIdentityGateway, KeycloakIdentityGateway>();
        services.AddHttpClient<IIdentityGateway, KeycloakIdentityGateway>(client =>
        {
            client.BaseAddress = new Uri($"{settings.Keycloak.Url}/admin/realms/{settings.Keycloak.Realm}/");
        });

        services.AddScoped<IClientAuthenticatorGateway, KeycloakClientAuthenticatorGateway>();
        services.AddHttpClient<IClientAuthenticatorGateway, KeycloakClientAuthenticatorGateway>(client =>
        {
            client.BaseAddress = new Uri($"{settings.Keycloak.Url}/realms/{settings.Keycloak.Realm}/");
        });
    }
}