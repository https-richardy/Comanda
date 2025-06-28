namespace Comanda.Application.Payloads;

public sealed record EnrollmentCredentials : IRequest<Result>
{
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
    public AccountType Type { get; set; }
}