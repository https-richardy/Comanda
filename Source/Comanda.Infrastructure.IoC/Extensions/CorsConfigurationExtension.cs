namespace Comanda.Infrastructure.IoC.Extensions;

[ExcludeFromCodeCoverage]
public static class CorsConfigurationExtension
{
    public static void AddCorsConfiguration(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader();
            });
        });
    }
}