namespace Comanda.Application.Payloads;

public sealed record LogoutRequest : IRequest<Result>
{
    public string RefreshToken { get; init; } = default!;
}