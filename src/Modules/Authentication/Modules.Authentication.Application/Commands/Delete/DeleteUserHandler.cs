using CodeUp.Common.Abstractions;
using CodeUp.Common.Notifications;
using CodeUp.Common.Responses;
using CodeUp.IntegrationEvents.Authentication;
using CodeUp.MessageBus;
using Modules.Authentication.Domain.Models;
using Modules.Authentication.Domain.Repositories;

namespace Modules.Authentication.Application.Commands.Delete;

public sealed class DeleteUserHandler(INotificator notificator,
                                      IUserRepository userRepository,
                                      IUnitOfWork uow,
                                      IMessageBus bus)
                                    : CommandHandler<DeleteUserCommand, DeleteUserResponse>(notificator)
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IUnitOfWork _uow = uow;
    private readonly IMessageBus _bus = bus;

    public override async Task<Response<DeleteUserResponse>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId);
        if (user is null)
        {
            Notify("User not found");
            return Response<DeleteUserResponse>.Failure(GetNotifications(), code: 404);
        }

        if (!await DeleteUserAsync(user))
        {
            Notify("Fail to delete user from base");
            return Response<DeleteUserResponse>.Failure(GetNotifications());
        }

        await _bus.PublishAsync<UserDeletedIntegrationEvent>(new(user.Id));

        return Response<DeleteUserResponse>.Success(null, 204);
    }

    private async Task<bool> DeleteUserAsync(User user)
    {
        await _userRepository.DeleteAsync(user);
        var userRoles = await _userRepository.GetUserRolesAsync(user.Id);
        userRoles.ForEach(_userRepository.DeleteUserRoles);
        return await _uow.SaveChangesAsync();
    }
}