using CodeUp.Common.Abstractions;
using CodeUp.Common.Notifications;
using CodeUp.Common.Responses;
using Modules.Authentication.Domain.Repositories;

namespace Modules.Authentication.Application.Commands.Delete;

public sealed class DeleteUserHandler(INotificator notificator,
                                      IUserRepository userRepository) 
                                    : CommandHandlerBase<DeleteUserCommand, DeleteUserResponse>(notificator)
{
    private readonly IUserRepository _userRepository = userRepository;
    public override async Task<Response<DeleteUserResponse>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId);
        if(user is null)
        {
            Notify("User not found");
            return Response<DeleteUserResponse>.Failure(GetNotifications(), "Invalid Operation", 404);
        }

        await _userRepository.DeleteAsync(user);

        return Response<DeleteUserResponse>.Success(null, 204);
    }
}
