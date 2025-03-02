using CodeUp.Common.Abstractions;
using Modules.Authentication.Application.DTOs;

namespace Modules.Authentication.Application.Commands.Login;
public record LoginUserCommand(string Email, string Password) : Command<LoginResponseDTO>;