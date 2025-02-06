using FluentValidation;
using System.Text.RegularExpressions;

namespace Modules.Authentication.Application.Commands.Register;

public sealed class RegisterUserValidation : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserValidation()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("The {PropertyName} field cannot be empty.")
            .Length(2, 100).WithMessage("The {PropertyName} must be between 2 and 100 characters.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("The {PropertyName} field cannot be empty.")
            .Length(2, 100).WithMessage("The {PropertyName} must be between 2 and 100 characters.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("The {PropertyName} field cannot be empty.")
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("The {PropertyName} field cannot be empty.")
            .Must(IsValidPhoneNumber).WithMessage("Invalid phone number format.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("The password field cannot be empty.")
            .MinimumLength(8).WithMessage("The password must be at least 8 characters long.")
            .Must(HasUpperCase).WithMessage("The password must contain at least one uppercase letter.")
            .Must(HasLowerCase).WithMessage("The password must contain at least one lowercase letter.")
            .Must(HasDigit).WithMessage("The password must contain at least one digit.")
            .Must(HasSpecialCharacter).WithMessage("The password must contain at least one special character (!@#$%^&* etc.).");
    }

    private static bool HasUpperCase(string password) => password.Any(char.IsUpper);

    private static bool HasLowerCase(string password) => password.Any(char.IsLower);

    private static bool HasDigit(string password) => password.Any(char.IsDigit);

    private static bool HasSpecialCharacter(string password)
    {
        var specialCharRegex = new Regex(@"[!@#$%^&*(),.?""{}|<>]");
        return specialCharRegex.IsMatch(password);
    }

    private static bool IsValidPhoneNumber(string phoneNumber)
    {
        var phoneRegex = new Regex(@"^\(?\d{2}\)?\s?\d{4,5}-\d{4}$");
        return phoneRegex.IsMatch(phoneNumber);
    }
}
