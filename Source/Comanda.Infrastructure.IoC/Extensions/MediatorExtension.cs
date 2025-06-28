namespace Comanda.Infrastructure.IoC.Extensions;

[ExcludeFromCodeCoverage]
public static class MediatorExtension
{
    public static void AddMediator(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssemblyContaining<AuthenticationHandler>();
        });
    }
}