using System.Security.Claims;

namespace Comanda.Infrastructure.Providers;

public sealed class HttpContextAuthenticatedUserProvider(IHttpContextAccessor contextAccessor) : IAuthenticatedUserProvider
{
    public Task<User> GetUserAsync()
    {
        var httpContext = contextAccessor.HttpContext;
        if (httpContext == null || httpContext.User.Identity?.IsAuthenticated != true)
        {
            return Task.FromResult(new User());
        }

        var claimsPrincipal = httpContext.User;
        var userIdClaim = claimsPrincipal.FindFirst("sub")?.Value;

        var email = claimsPrincipal.FindFirst("email")?.Value ?? "";
        var username = claimsPrincipal.FindFirst("preferred_username")?.Value ?? "";

        var identity = claimsPrincipal.Identities.FirstOrDefault();
        var userId = Guid.Parse(identity!.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value!);

        var user = new User
        {
            Id = userId,
            Email = identity!.Name!,
            Name = identity.Name!
        };

        return Task.FromResult(user);
    }
}
