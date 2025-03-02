using CodeUp.Common.Abstractions;
using CodeUp.Common.Notifications;
using CodeUp.Common.Responses;
using Modules.Authentication.Application.Services;
using Modules.Authentication.Domain.Repositories;

namespace Modules.Authentication.Application.Commands.ResetPassword;

public sealed class ResetPasswordHandler(INotificator notificator,
                                         IUserRepository userRepository,
                                         IHasherService hasherService,
                                         IUnitOfWork uow)
                                       : CommandHandler<ResetPasswordCommand,
                                         ResetPasswordResponse>(notificator)
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IHasherService _hasherService = hasherService;
    private readonly IUnitOfWork _uow = uow;

    public override async Task<Response<ResetPasswordResponse>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);
        if (user is null)
        {
            Notify("User not found");
            return Response<ResetPasswordResponse>.Failure(GetNotifications());
        }

        if (!await TokenIsValid(user.Id, request.Token))
            return Response<ResetPasswordResponse>.Failure(GetNotifications());

        user.UpdatePassword(_hasherService.HashPassword(request.Password));
        _userRepository.Update(user);

        if (!await _uow.SaveChangesAsync())
        {
            Notify("Fail to persist data");
            return Response<ResetPasswordResponse>.Failure(GetNotifications());
        }

        return Response<ResetPasswordResponse>.Success(null, 204);
    }

    private async Task<bool> TokenIsValid(Guid userId, string token)
    {
        var userToken = await _userRepository.GetTokenByUserIdAsync(userId);
        if (userToken is null)
        {
            Notify("Token not found");
            return false;
        }

        if (userToken.Token != token && userToken.ExpiresAt > DateTime.Now)
        {
            Notify("Invalid token");
            return false;
        }

        _userRepository.DeleteUserToken(userToken);
        return true;
    }
}