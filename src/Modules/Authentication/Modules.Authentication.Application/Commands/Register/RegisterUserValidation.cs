using FluentValidation;

namespace Modules.Authentication.Application.Commands.Register;

public sealed class RegisterUserValidation : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserValidation()
    {
        RuleFor(x => x.FirstName).NotEmpty()
            .WithMessage("The {PropertyName} field can not be empty")
            .Length(2, 100).WithMessage("The {PropertyName} field should have 2 - 100 caracters");

        RuleFor(x => x.LastName).NotEmpty()
            .WithMessage("The {PropertyName} field can not be empty")
            .Length(2, 100).WithMessage("The {PropertyName} field should have 2 - 100 caracters");
    }
}
