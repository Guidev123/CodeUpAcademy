using FluentValidation;

namespace Modules.Authentication.Application.Commands.ResetPassword;

public sealed class ResetPasswordValidation : AbstractValidator<ResetPasswordCommand>
{
    public ResetPasswordValidation()
    {
    }
}