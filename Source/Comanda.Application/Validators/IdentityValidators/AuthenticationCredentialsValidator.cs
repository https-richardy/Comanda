namespace Comanda.Application.Validators;

public sealed class AuthenticationCredentialsValidator : AbstractValidator<AuthenticationCredentials>
{
    public AuthenticationCredentialsValidator()
    {
        RuleFor(credential => credential.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Email must be a valid address.");

        RuleFor(credential => credential.Password)
            .NotEmpty()
            .WithMessage("Password is required.");
    }
}