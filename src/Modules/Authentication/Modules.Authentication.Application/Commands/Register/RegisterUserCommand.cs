using CodeUp.Common.Abstractions;
using Modules.Authentication.Application.DTOs;

namespace Modules.Authentication.Application.Commands.Register;

public record RegisterUserCommand(
    string FirstName, string LastName,
    string Email, string Phone,
    DateTime BirthDate, string PasswordHash
    ): CommandBase<LoginResponseDTO>;
