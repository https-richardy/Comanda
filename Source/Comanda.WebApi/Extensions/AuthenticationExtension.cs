namespace Comanda.WebApi.Extensions;

[ExcludeFromCodeCoverage]
public static class AuthenticationExtension
{
    public static void AddBearerAuthentication(this IServiceCollection services)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = "http://localhost:8080/realms/master";
                options.RequireHttpsMetadata = false;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    RoleClaimType = ClaimTypes.Role, // veja que alterei para ClaimTypes.Role
                    NameClaimType = "preferred_username",
                    ValidateAudience = false // <- desativa validação do audience
                };

                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        var identity = context.Principal?.Identity as ClaimsIdentity;
                        if (identity is null)
                            return Task.CompletedTask;

                        // Mapear roles do realm_access.roles
                        var realmAccessClaim = context.Principal?.FindFirst("realm_access")?.Value;
                        if (!string.IsNullOrWhiteSpace(realmAccessClaim))
                        {
                            using var json = JsonDocument.Parse(realmAccessClaim);

                            if (json.RootElement.TryGetProperty("roles", out var rolesElement) &&
                                rolesElement.ValueKind == JsonValueKind.Array)
                            {
                                foreach (var role in rolesElement.EnumerateArray())
                                {
                                    var roleValue = role.GetString();
                                    if (!string.IsNullOrWhiteSpace(roleValue))
                                        identity.AddClaim(new Claim(ClaimTypes.Role, roleValue));
                                }
                            }
                        }

                        // Mapear roles do resource_access.comanda-webapi.roles
                        var resourceAccessClaim = context.Principal?.FindFirst("resource_access")?.Value;
                        if (!string.IsNullOrWhiteSpace(resourceAccessClaim))
                        {
                            using var json = JsonDocument.Parse(resourceAccessClaim);
                            if (json.RootElement.TryGetProperty("comanda-webapi", out var clientAccess))
                            {
                                if (clientAccess.TryGetProperty("roles", out var rolesElement) &&
                                    rolesElement.ValueKind == JsonValueKind.Array)
                                {
                                    foreach (var role in rolesElement.EnumerateArray())
                                    {
                                        var roleValue = role.GetString();
                                        if (!string.IsNullOrWhiteSpace(roleValue))
                                            identity.AddClaim(new Claim(ClaimTypes.Role, roleValue));
                                    }
                                }
                            }
                        }

                        return Task.CompletedTask;
                    }
                };
            });
    }
}