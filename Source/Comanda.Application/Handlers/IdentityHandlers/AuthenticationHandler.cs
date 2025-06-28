namespace Comanda.Application.Handlers;

public sealed class AuthenticationHandler(IAuthenticationGateway authenticationGateway) :
    IRequestHandler<AuthenticationCredentials, Result<AuthenticationResult>>
{
    public async Task<Result<AuthenticationResult>> Handle(
        AuthenticationCredentials request,
        CancellationToken cancellationToken
    )
    {
        return await authenticationGateway.AuthenticateAsync(request);
    }
}


