namespace Comanda.Infrastructure.IoC.Extensions;

[ExcludeFromCodeCoverage]
public static class ServicesExtension
{
    public static void AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        var settings = services.ConfigureSettings(configuration);

        services.AddDataPersistence(settings);
        services.AddExternalGateways(settings);
        services.AddHealthCheckers();

        services.AddProviders();
        services.AddMediator();
        services.AddValidators();
        services.AddApplicationServices();
        services.AddHttpContextAccessor();
    }
}