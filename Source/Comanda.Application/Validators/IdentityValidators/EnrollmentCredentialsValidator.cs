namespace Comanda.Application.Validators;

public sealed class EnrollmentCredentialsValidator : AbstractValidator<EnrollmentCredentials>
{
    public EnrollmentCredentialsValidator()
    {
        RuleFor(request => request.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Email must be a valid address.");

        RuleFor(request => request.Password)
            .NotEmpty()
            .WithMessage("Password is required.")
            .MinimumLength(8)
            .WithMessage("Password must be at least 8 characters long.");
    }
}