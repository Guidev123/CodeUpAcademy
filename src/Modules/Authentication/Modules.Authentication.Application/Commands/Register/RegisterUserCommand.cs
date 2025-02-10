using CodeUp.Common.Abstractions;
using Modules.Authentication.Application.DTOs;

namespace Modules.Authentication.Application.Commands.Register;

public record RegisterUserCommand(
    string FirstName, string LastName,
    string Email, string Phone,
    string Document, string ProfilePicture,
    DateTime BirthDate, string Password,
    string ConfirmPassword
    ): Command<LoginResponseDTO>;
