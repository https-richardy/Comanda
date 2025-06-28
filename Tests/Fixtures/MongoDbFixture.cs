namespace Comanda.TestSuite.Fixtures;

public class MongoDbFixture : IAsyncLifetime
{
    public IMongoDatabase Database { get; private set; } = default!;
    public IMongoClient Client { get; private set; } = default!;
    private readonly IContainer _container;

    public MongoDbFixture()
    {
        _container = new ContainerBuilder()
            .WithImage("mongo:latest")
            .WithCleanUp(true)
            .WithExposedPort(27017)
            .WithPortBinding(0, 27017)
            .WithEnvironment("MONGO_INITDB_ROOT_USERNAME", "admin")
            .WithEnvironment("MONGO_INITDB_ROOT_PASSWORD", "admin")
            .Build();
    }

    public async Task DisposeAsync()
    {
        await _container.StopAsync();
    }

    public async Task InitializeAsync()
    {
        await _container.StartAsync();

        var hostPort = _container.GetMappedPublicPort(27017);
        var databaseName = "comanda";

        var connectionString = $"mongodb://admin:admin@localhost:{hostPort}/{databaseName}?authSource=admin";

        Client = new MongoClient(connectionString);
        Database = Client.GetDatabase(databaseName);

        MongoMapping.Register();
    }
}