namespace Comanda.Application.Payloads;

public sealed record RefreshTokenRequest : IRequest<Result<AuthenticationResult>>
{
    public string RefreshToken { get; init; } = default!;
}