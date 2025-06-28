namespace Comanda.Application.Payloads;

public sealed record AuthenticationCredentials : IRequest<Result<AuthenticationResult>>
{
    public string Email { get; init; } = default!;
    public string Password { get; init; } = default!;
}