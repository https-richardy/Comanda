namespace Comanda.Application.Gateways;

public interface IClientAuthenticatorGateway
{
    Task<Result<AuthenticationResult>> AuthenticateAsync();
}