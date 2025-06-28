namespace Comanda.Application.Gateways;

public interface IAuthenticationGateway
{
    Task<Result<AuthenticationResult>> AuthenticateAsync(AuthenticationCredentials credentials);
    Task<Result<AuthenticationResult>> RefreshTokenAsync(string refreshToken);
    Task<Result> LogoutAsync(string refreshToken);
}