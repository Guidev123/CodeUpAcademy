using FluentValidation;
using System.Text.RegularExpressions;

namespace Modules.Students.Application.Commands.Create;

public sealed class CreateStudentValidation : AbstractValidator<CreateStudentCommand>
{
    public CreateStudentValidation()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("The {PropertyName} field cannot be empty.");

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

        RuleFor(x => x.Document)
            .NotEmpty().WithMessage("The {PropertyName} field cannot be empty.")
            .Must(IsValidDocument).WithMessage("Invalid document numbers format.");
    }

    public static string JustNumbers(string input) => new([.. input.Where(char.IsDigit)]);

    public static bool IsValidDocument(string number)
    {
        if (string.IsNullOrWhiteSpace(number))
            return false;

        number = JustNumbers(number);

        if (number.Length != 11)
            return false;


        var numbers = number.Select(digit => int.Parse(digit.ToString())).ToArray();

        int sum = 0;
        for (int i = 0; i < 9; i++)
            sum += numbers[i] * (10 - i);

        int result = sum % 11;
        int firstDigit = result < 2 ? 0 : 11 - result;
        if (numbers[9] != firstDigit)
            return false;

        sum = 0;
        for (int i = 0; i < 10; i++)
            sum += numbers[i] * (11 - i);

        result = sum % 11;
        int secondDigit = result < 2 ? 0 : 11 - result;
        if (numbers[10] != secondDigit)
            return false;

        return true;
    }

    private static bool IsValidPhoneNumber(string phoneNumber)
    {
        var phoneRegex = new Regex(@"^\(?\d{2}\)?\s?\d{4,5}-\d{4}$");
        return phoneRegex.IsMatch(phoneNumber);
    }
}
