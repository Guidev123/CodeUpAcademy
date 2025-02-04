using FluentValidation;

namespace Modules.Authentication.Application.Commands.Register;

public class RegisterUserValidation : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserValidation()
    {

    }
}
