using CodeUp.Common.Abstractions;

namespace Modules.Authentication.Application.Commands.ForgotPassword;

public record ForgotPasswordCommand(string Email, string ClientUrlResetPassword) : CommandBase<ForgotPasswordResponse>;
