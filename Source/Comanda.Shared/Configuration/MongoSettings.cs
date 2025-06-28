namespace Comanda.Shared.Configuration;

public sealed class MongoSettings
{
    public string ConnectionString { get; set; } = default!;
    public string DatabaseName { get; set; } = default!;
}