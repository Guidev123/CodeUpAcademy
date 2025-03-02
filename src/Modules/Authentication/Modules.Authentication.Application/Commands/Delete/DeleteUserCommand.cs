using CodeUp.Common.Abstractions;

namespace Modules.Authentication.Application.Commands.Delete;

public record DeleteUserCommand(Guid UserId) : Command<DeleteUserResponse>;