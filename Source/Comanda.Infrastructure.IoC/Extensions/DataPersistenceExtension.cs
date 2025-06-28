namespace Comanda.Infrastructure.IoC.Extensions;

[ExcludeFromCodeCoverage]
public static class DataPersistenceExtension
{
    public static void AddDataPersistence(this IServiceCollection services, ISettings settings)
    {
        MongoMapping.Register();

        services.AddSingleton<IMongoDatabase>(provider =>
        {
            var mongoClient = new MongoClient(settings.Mongo.ConnectionString);
            var database = mongoClient.GetDatabase(settings.Mongo.DatabaseName);

            return database;
        });

        services.AddScoped<IEstablishmentRepository, EstablishmentRepository>();
        services.AddScoped<IEstablishmentOwnerRepository, EstablishmentOwnerRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
    }
}