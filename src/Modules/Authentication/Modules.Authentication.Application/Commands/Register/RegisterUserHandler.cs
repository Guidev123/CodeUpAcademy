using CodeUp.Common.Abstractions;
using CodeUp.Common.Notifications;
using CodeUp.Common.Responses;
using Modules.Authentication.Application.DTOs;
using Modules.Authentication.Application.Mappers;
using Modules.Authentication.Application.Services;
using Modules.Authentication.Domain.Entities;
using Modules.Authentication.Domain.Enums;
using Modules.Authentication.Domain.Repositories;

namespace Modules.Authentication.Application.Commands.Register;

public sealed class RegisterUserHandler(IUserRepository userRepository,
                    ITokenService tokenService,
                    IPasswordHasherService passwordHasher,
                    IUnitOfWork uow,
                    INotificator notificator)
                  : CommandHandlerBase<RegisterUserCommand, LoginResponseDTO>(notificator)
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly ITokenService _tokenService = tokenService;
    private readonly IPasswordHasherService _passwordHasher = passwordHasher;
    private readonly IUnitOfWork _uow = uow;
    private readonly INotificator _notificator = notificator;

    public override async Task<Response<LoginResponseDTO>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var userExists = await _userRepository.GetByEmailAsync(request.Email);
        if (userExists is not null)
        {
            Notify("User already exists");
            return new(null, 400, "Invalid Operation", _notificator.GetNotifications().Select(n => n.Message).ToList());
        }

        if (!ExecuteValidation(new RegisterUserValidation(), request))
            return new(null, 400, "Invalid Operation", _notificator.GetNotifications().Select(n => n.Message).ToList());

        var user = request.MapToEntity(_passwordHasher.HashPassword(request.Password));

        var role = User.AddRole(user.Id, (long)SubscriptionTypeEnum.Free);

        await _userRepository.CreateAsync(user);
        await _userRepository.CreateUserRoleAsync(role);

        if (!await _uow.SaveChangesAsync())
        {
            Notify("Fail to persist data");
            return new(null, 400, "Invalid Operation", _notificator.GetNotifications().Select(n => n.Message).ToList());
        }

        var token = await _tokenService.GenerateJWT(request.Email);
        if (!token.IsSuccess)
        {
            Notify("Fail to authenticate user");
            return new(null, 400, "Invalid Operation", _notificator.GetNotifications().Select(n => n.Message).ToList());
        }

        await RegisterLoginAsync(user);
        return new(token.Data, 201);
    }

    private async Task RegisterLoginAsync(User user)
    {
        user.RegisterLogin();
        _userRepository.Update(user);
        await _uow.SaveChangesAsync();
    }
}
