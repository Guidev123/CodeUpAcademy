using CodeUp.Common.Abstractions;
using CodeUp.Common.Notifications;
using CodeUp.Common.Responses;
using Modules.Authentication.Domain.Repositories;

namespace Modules.Authentication.Application.Commands.ForgotPassword;

public sealed class ForgotPasswordHandler(INotificator notificator,
                                          IUserRepository userRepository)
                                        : CommandHandlerBase<ForgotPasswordCommand,
                                          ForgotPasswordResponse>(notificator)
{
    private readonly IUserRepository _userRepository = userRepository;
    public override async Task<Response<ForgotPasswordResponse>> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        if (!ExecuteValidation(new ForgotPasswordValidation(), request))
            return Response<ForgotPasswordResponse>.Failure(GetNotifications(), "Invalid Operation");

        var user = await _userRepository.GetByEmailAsync(request.Email);
        if (user is null)
        {
            Notify("User not found");
            return Response<ForgotPasswordResponse>.Failure(GetNotifications(), "Invalid Operation", 404);
        }

        return new();
    }
}
