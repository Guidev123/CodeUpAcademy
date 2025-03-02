using CodeUp.Common.Abstractions;
using Modules.Authentication.Application.DTOs;

namespace Modules.Authentication.Application.Commands.RefreshToken;

public record RefreshTokenCommand(string RefreshToken) : Command<LoginResponseDTO>;