namespace Comanda.Infrastructure.IoC.Extensions;

[ExcludeFromCodeCoverage]
public static class ServicesExtension
{
    public static void AddServices(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
    {
        var settings = services.ConfigureSettings(configuration);

        if (environment.IsProduction())
        {
            settings.OverrideSettingsWithEnvironmentVariables();
        }

        services.AddDataPersistence(settings);
        services.AddExternalGateways(settings);
        services.AddHealthCheckers();

        services.AddProviders();
        services.AddMediator();
        services.AddValidators();
        services.AddApplicationServices();

        services.AddCorsConfiguration();
        services.AddHttpContextAccessor();
    }
}