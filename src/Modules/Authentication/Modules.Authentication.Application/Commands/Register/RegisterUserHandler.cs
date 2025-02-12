using CodeUp.Common.Abstractions;
using CodeUp.Common.Notifications;
using CodeUp.Common.Responses;
using CodeUp.IntegrationEvents;
using CodeUp.IntegrationEvents.Authentication;
using CodeUp.MessageBus;
using Modules.Authentication.Application.DTOs;
using Modules.Authentication.Application.Mappers;
using Modules.Authentication.Application.Services;
using Modules.Authentication.Domain.Enums;
using Modules.Authentication.Domain.Models;
using Modules.Authentication.Domain.Repositories;

namespace Modules.Authentication.Application.Commands.Register;

public sealed class RegisterUserHandler(IUserRepository userRepository,
                    ITokenService tokenService,
                    IHasherService hasherService,
                    IUnitOfWork uow,
                    INotificator notificator,
                    IMessageBus bus)
                  : CommandHandler<RegisterUserCommand, LoginResponseDTO>(notificator)
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly ITokenService _tokenService = tokenService;
    private readonly IHasherService _hasherService = hasherService;
    private readonly IUnitOfWork _uow = uow;
    private readonly IMessageBus _bus = bus;

    public override async Task<Response<LoginResponseDTO>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var userExists = await _userRepository.GetByEmailAsync(request.Email);
        if (userExists is not null)
        {
            Notify("User already exists");
            return Response<LoginResponseDTO>.Failure(GetNotifications(), "Invalid Operation");
        }

        if (!ExecuteValidation(new RegisterUserValidation(), request))
            return Response<LoginResponseDTO>.Failure(GetNotifications(), "Invalid Operation");

        var user = request.MapToEntity(_hasherService.HashPassword(request.Password));
        await _userRepository.CreateAsync(user);

        var student = await RegisterStudentAsync(request, user);
        if (!student.IsValid)
        {
            student.Errors!.ToList().ForEach(Notify);
            return Response<LoginResponseDTO>.Failure(GetNotifications(), "Invalid Operation");
        }

        var role = User.AddRole(user.Id, (long)SubscriptionTypeEnum.Free);

        await _userRepository.CreateUserRoleAsync(role);

        if (!await _uow.SaveChangesAsync())
        {
            Notify("Fail to persist data");
            return Response<LoginResponseDTO>.Failure(GetNotifications(), "Invalid Operation");
        }

        var token = await _tokenService.GenerateJWT(request.Email);
        if (!token.IsSuccess)
        {
            Notify("Fail to authenticate user");
            return Response<LoginResponseDTO>.Failure(GetNotifications(), "Invalid Operation");
        }

        await RegisterLoginAsync(user);
        return Response<LoginResponseDTO>.Success(token.Data, 201);
    }

    private async Task<IntegrationEventResponseMessage> RegisterStudentAsync(RegisterUserCommand request, User user)
    {
        try
        {
            return await _bus.RequestAsync<RegisteredUserIntegrationEvent, IntegrationEventResponseMessage>(new RegisteredUserIntegrationEvent
                (user.Id, user.FirstName, user.LastName,
                 user.Email.Address, user.Phone.Number,
                 request.Document, request.ProfilePicture,
                 user.BirthDate, (int)SubscriptionTypeEnum.Free));
        }
        catch
        {
            Notify($"Something has failed during your register.");
            return new(false);
        }
    }

    private async Task RegisterLoginAsync(User user)
    {
        user.RegisterLogin();
        _userRepository.Update(user);
        await _uow.SaveChangesAsync();
    }
}
