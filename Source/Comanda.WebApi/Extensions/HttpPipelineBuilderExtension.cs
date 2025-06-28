namespace Comanda.WebApi.Extensions;

[ExcludeFromCodeCoverage]
public static class HttpPipelineBuilderExtension
{
    public static void BuildHttpPipeline(this IApplicationBuilder app, IWebHostEnvironment environment)
    {
        app.UseHttpsRedirection();

        app.UseRouting();
        app.UseCors();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        app.UseHealthChecks("/health", new HealthCheckOptions
        {
            Predicate = _ => true,
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });

        app.UseStaticFiles();
    }
}