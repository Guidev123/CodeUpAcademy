using CodeUp.Common.Abstractions;
using CodeUp.Common.Notifications;
using CodeUp.Common.Responses;
using Modules.Students.Application.Mappers;
using Modules.Students.Application.Services;
using Modules.Students.Domain.Repositories;

namespace Modules.Students.Application.Queries.GetById;

public sealed class GetStudentByIdHandler(
                    IStudentRepository studentRepository,
                    IUserService userService,
                    INotificator notificator)
                  : QueryHandler<GetStudentByIdQuery, GetStudentByIdResponse>(notificator)
{
    private readonly IStudentRepository _studentRepository = studentRepository;
    private readonly IUserService _userService = userService;

    public override async Task<Response<GetStudentByIdResponse>> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
    {
        var userId = _userService.GetUserId();
        if (userId is null || !userId.HasValue)
        {
            Notify("User not found.");
            return Response<GetStudentByIdResponse>.Failure(GetNotifications(), code: 404);
        }

        var student = await _studentRepository.GetByIdAsync(userId.Value);
        if (student is null)
        {
            Notify("Student not found.");
            return Response<GetStudentByIdResponse>.Failure(GetNotifications(), code: 404);
        }

        return Response<GetStudentByIdResponse>.Success(student.MapFromEntity());
    }
}