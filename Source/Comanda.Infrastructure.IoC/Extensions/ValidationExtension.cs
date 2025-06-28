namespace Comanda.Infrastructure.IoC.Extensions;

[ExcludeFromCodeCoverage]
public static class ValidationExtension
{
    public static void AddValidators(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation(options =>
        {
            options.DisableDataAnnotationsValidation = true;
        });

        services.AddScoped<IValidator<AuthenticationCredentials>, AuthenticationCredentialsValidator>();
        services.AddScoped<IValidator<EnrollmentCredentials>, EnrollmentCredentialsValidator>();

        services.AddScoped<IValidator<RefreshTokenRequest>, RefreshTokenRequestValidator>();
        services.AddScoped<IValidator<LogoutRequest>, LogoutRequestValidator>();
    }
}