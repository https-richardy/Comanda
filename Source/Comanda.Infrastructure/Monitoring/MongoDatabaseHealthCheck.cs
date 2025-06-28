namespace Comanda.Infrastructure.Monitoring;

public sealed class MongoDatabaseHealthCheck(IMongoDatabase database) : IHealthCheck
{
    public const string Label = "[Comanda.Infrastructure] MongoDatabase";

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            await database.ListCollectionNamesAsync(cancellationToken: cancellationToken);

            return HealthCheckResult.Healthy("MongoDB is reachable.", new Dictionary<string, object>
            {
                { "Database", database.DatabaseNamespace.DatabaseName }
            });
        }
        catch (Exception exception)
        {
            return HealthCheckResult.Unhealthy("Failed to connect to database.", exception);
        }
    }
}