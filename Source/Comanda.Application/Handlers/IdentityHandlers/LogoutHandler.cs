namespace Comanda.Application.Handlers;

public sealed class LogoutHandler(IAuthenticationGateway authenticationGateway) :
    IRequestHandler<LogoutRequest, Result>
{
    public async Task<Result> Handle(LogoutRequest request, CancellationToken cancellationToken)
    {
        return await authenticationGateway.LogoutAsync(request.RefreshToken);
    }
}
