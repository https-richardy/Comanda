namespace Comanda.WebApi;

internal static class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        Console.WriteLine("Starting Comanda Web API...");
        Console.WriteLine($"MONGO CONNECTIONSTRING: {Environment.GetEnvironmentVariable("Mongo__ConnectionString")}");
        Console.WriteLine($"MONGO DATABASE: {Environment.GetEnvironmentVariable("Mongo__DatabaseName")}");

        var environment = builder.Environment;
        var configuration = builder.Configuration;

        builder.Configuration.AddEnvironmentVariables();

        builder.Services.AddControllers();
        builder.Services.AddOpenApi();

        var app = builder.Build();

        app.BuildHttpPipeline(environment);
        app.Run();
    }
}
