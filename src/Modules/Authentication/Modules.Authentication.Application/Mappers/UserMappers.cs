using Modules.Authentication.Application.Commands.Register;
using Modules.Authentication.Domain.Models;

namespace Modules.Authentication.Application.Mappers;

public static class UserMappers
{
    public static User MapToEntity(this RegisterUserCommand command, string passwordHash)
        => new(command.FirstName, command.LastName, command.Email, command.Phone, command.BirthDate, passwordHash);
}
