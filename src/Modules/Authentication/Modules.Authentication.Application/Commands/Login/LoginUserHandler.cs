﻿using CodeUp.Common.Abstractions;
using CodeUp.Common.Notifications;
using CodeUp.Common.Responses;
using Modules.Authentication.Application.DTOs;
using Modules.Authentication.Application.Services;
using Modules.Authentication.Domain.Entities;
using Modules.Authentication.Domain.Repositories;

namespace Modules.Authentication.Application.Commands.Login;

public sealed class LoginUserHandler(INotificator notificator,
                                     IUserRepository userRepository,
                                     ITokenService tokenService,
                                     IPasswordHasherService passwordHasherService,
                                     IUnitOfWork uow)
                                   : CommandHandlerBase<LoginUserCommand, LoginResponseDTO>(notificator)
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly ITokenService _tokenService = tokenService;
    private readonly INotificator _notificator = notificator;
    private readonly IPasswordHasherService _passwordHasherService = passwordHasherService;
    private readonly IUnitOfWork _uow = uow;

    public override async Task<Response<LoginResponseDTO>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        if (!ExecuteValidation(new LoginUserValidation(), request))
            return Response<LoginResponseDTO>.Failure(GetNotifications(), "Invalid Operation");

        var user = await _userRepository.GetByEmailAsync(request.Email);
        if (user is null)
        {
            Notify("User not found");
            return Response<LoginResponseDTO>.Failure(GetNotifications(), "Invalid Operation");
        }

        if (!user.UserIsAbleToLogin())
        {
            Notify("You have been temporarily blocked due to multiple failed login attempts with wrong credentials");
            return Response<LoginResponseDTO>.Failure(GetNotifications(), "Invalid Operation", 404);
        }

        var passwordMatch = _passwordHasherService.VerifyPassword(request.Password, user.PasswordHash);
        if (!passwordMatch)
        {
            await UpdateUserAsync(user);

            Notify("User credentials are wrong");
            return Response<LoginResponseDTO>.Failure(GetNotifications(), "Invalid Operation", 404);
        }

        var token = await _tokenService.GenerateJWT(request.Email);
        if (!token.IsSuccess)
        {
            Notify("Fail to authenticate user");
            return Response<LoginResponseDTO>.Failure(GetNotifications(), "Invalid Operation");
        }

        await RegisterLoginAsync(user);
        return Response<LoginResponseDTO>.Success(token.Data);
    }

    private async Task RegisterLoginAsync(User user)
    {
        user.RegisterLogin();
        _userRepository.Update(user);
        await _uow.SaveChangesAsync();
    }

    private async Task UpdateUserAsync(User user)
    {
        user.AddAccessFailedCount();
        _userRepository.Update(user);
        if (!await _uow.SaveChangesAsync())
            Notify("Fail to persist updates in database");
    }
}
