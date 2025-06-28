namespace Comanda.Infrastructure.IoC.Extensions;

[ExcludeFromCodeCoverage]
public static class HealthCheckersExtension
{
    public static void AddHealthCheckers(this IServiceCollection services)
    {
        services.AddHealthChecks()
            .AddCheck<MongoDatabaseHealthCheck>(MongoDatabaseHealthCheck.Label, failureStatus: HealthStatus.Unhealthy);
    }
}