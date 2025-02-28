using CodeUp.Common.Abstractions;
using CodeUp.Common.Notifications;
using CodeUp.Common.Responses;
using Modules.Authentication.Application.Mappers;
using Modules.Authentication.Application.Services;
using Modules.Authentication.Domain.Repositories;

namespace Modules.Authentication.Application.Queries.GetById;

public sealed class GetUserByIdHandler(IUserRepository userRepository,
                                       INotificator notificator,
                                       IUserService userService) : QueryHandler<GetUserByIdQuery, GetUserByIdResponse>(notificator)
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IUserService _userService = userService;

    public override async Task<Response<GetUserByIdResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var userId = _userService.GetUserId();
        if (!userId.HasValue)
        {
            Notify("User not found");
            return Response<GetUserByIdResponse>.Failure(GetNotifications(), code: 404);
        }

        var user = await _userRepository.GetByIdAsync(userId.Value);
        if(user is null)
        {
            Notify("User not found");
            return Response<GetUserByIdResponse>.Failure(GetNotifications(), code: 404);
        }

        var userClaims = _userService.GetUserClaims();

        var response = UserMappers.MapFromEntity(user, userClaims);

        return Response<GetUserByIdResponse>.Success(response);
    }
}
