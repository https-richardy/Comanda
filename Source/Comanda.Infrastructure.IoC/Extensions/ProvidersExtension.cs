namespace Comanda.Infrastructure.IoC.Extensions;

[ExcludeFromCodeCoverage]
public static class ProvidersExtension
{
    public static void AddProviders(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticatedUserProvider, HttpContextAuthenticatedUserProvider>();
    }
}