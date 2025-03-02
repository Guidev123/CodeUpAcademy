using CodeUp.Common.Abstractions;
using CodeUp.Common.Notifications;
using CodeUp.Common.Responses;
using Modules.Authentication.Application.DTOs;
using Modules.Authentication.Application.Services;

namespace Modules.Authentication.Application.Commands.RefreshToken;

public sealed class RefreshTokenHandler(
                    INotificator notificator,
                    ITokenService tokenService)
                  : CommandHandler<RefreshTokenCommand, LoginResponseDTO>(notificator)
{
    private readonly ITokenService _tokenService = tokenService;

    public override async Task<Response<LoginResponseDTO>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.RefreshToken))
        {
            Notify("Invalid Refresh Token.");
            return Response<LoginResponseDTO>.Failure(GetNotifications());
        }

        var refreshToken = await _tokenService.GetRefreshTokenAsync(Guid.Parse(request.RefreshToken));
        if (!refreshToken.IsSuccess)
        {
            Notify("Refresh Token not found.");
            return Response<LoginResponseDTO>.Failure(GetNotifications());
        }

        var token = await _tokenService.GenerateJWT(refreshToken.Data!.UserEmail);
        if (!token.IsSuccess || token.Data is null)
        {
            Notify("Fail to generate token.");
            return Response<LoginResponseDTO>.Failure(GetNotifications());
        }

        return Response<LoginResponseDTO>.Success(token.Data, 201);
    }
}