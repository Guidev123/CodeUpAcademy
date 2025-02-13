using FluentValidation;
using System.Text.RegularExpressions;

namespace Modules.Students.Application.Commands.AddProfilePicture;

public class AddProfilePictureValidation : AbstractValidator<AddProfilePictureCommand>
{
    public AddProfilePictureValidation()
    {
        RuleFor(x => x.ProfilePicture)
            .NotEmpty().WithMessage("The {PropertyName} can not be empty")
            .Must(BeAValidBase64).WithMessage("The {PropertyName} should be a valid base64.");
    }

    private bool BeAValidBase64(string base64String)
    {
        if (string.IsNullOrWhiteSpace(base64String))
            return false;

        var base64Regex = new Regex(@"^[A-Za-z0-9+/]*={0,2}$", RegexOptions.None);

        return base64String.Length % 4 == 0 && base64Regex.IsMatch(base64String);
    }
}