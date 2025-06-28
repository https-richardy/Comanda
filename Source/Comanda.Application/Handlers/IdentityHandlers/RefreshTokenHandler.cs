namespace Comanda.Application.Handlers;

public sealed class RefreshTokenHandler(IAuthenticationGateway authenticationGateway) :
    IRequestHandler<RefreshTokenRequest, Result<AuthenticationResult>>
{
    public async Task<Result<AuthenticationResult>> Handle(RefreshTokenRequest request, CancellationToken cancellationToken)
    {
        return await authenticationGateway.RefreshTokenAsync(request.RefreshToken);
    }
}
