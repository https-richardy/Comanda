namespace Comanda.WebApi;

internal static class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var environment = builder.Environment;
        var configuration = builder.Configuration;

        builder.Configuration.AddEnvironmentVariables();

        builder.Services.AddControllers();
        builder.Services.AddOpenApi();

        builder.Services.AddBearerAuthentication();
        builder.Services.AddServices(configuration);

        var app = builder.Build();

        app.BuildHttpPipeline(environment);
        app.Run();
    }
}
