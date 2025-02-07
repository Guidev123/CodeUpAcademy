using FluentValidation;

namespace Modules.Authentication.Application.Commands.ForgotPassword;

public sealed class ForgotPasswordValidation : AbstractValidator<ForgotPasswordCommand>
{
    public ForgotPasswordValidation()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("{PropertyName} is required")
            .EmailAddress()
            .WithMessage("{PropertyName} is not valid");

        RuleFor(x => x.ClientUrlResetPassword)
            .NotEmpty()
            .WithMessage("{PropertyName} is required")
            .Must(x => x.Contains("http://") || x.Contains("https://"))
            .WithMessage("{PropertyName} must be a valid URL");
    }
}
