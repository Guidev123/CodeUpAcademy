using CodeUp.Common.Abstractions;

namespace Modules.Authentication.Application.Commands.ResetPassword;

public record ResetPasswordCommand(
              string Password, string ConfirmPassword,
              string Email, string Token
              ) : Command<ResetPasswordResponse>;