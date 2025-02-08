using CodeUp.Common.Abstractions;
using CodeUp.Common.Notifications;
using CodeUp.Common.Responses;
using CodeUp.Email;
using CodeUp.Email.Models;
using Microsoft.AspNetCore.WebUtilities;
using Modules.Authentication.Application.Services;
using Modules.Authentication.Domain.Repositories;

namespace Modules.Authentication.Application.Commands.ForgotPassword;

public sealed class ForgotPasswordHandler(INotificator notificator,
                                          IUserRepository userRepository,
                                          IEmailService emailService,
                                          IHasherService hasherService,
                                          IUnitOfWork uow)
                                        : CommandHandlerBase<ForgotPasswordCommand,
                                          ForgotPasswordResponse>(notificator)
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IEmailService _emailService = emailService;
    private readonly IHasherService _hasherService = hasherService;
    private readonly IUnitOfWork _uow = uow;
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

        var token = _hasherService.GenerateToken();
        var param = new Dictionary<string, string?>
        {
            {"token", token },
            {"email", request.Email}
        };

        var callback = QueryHelpers.AddQueryString(request.ClientUrlResetPassword, param);
        var message = new EmailMessage(user.Email.Address!, "Reset password token", callback);

        await _userRepository.CreateUserTokenAsync(new(user.Id, token, user.Email.Address));
        await _uow.SaveChangesAsync();

        await _emailService.SendAsync(message);

        return Response<ForgotPasswordResponse>.Success(null, 204);
    }
}
