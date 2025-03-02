using CodeUp.Common.Abstractions;
using CodeUp.Common.Notifications;
using CodeUp.Common.Responses;
using Modules.Students.Domain.Repositories;

namespace Modules.Students.Application.Commands.Delete;

public sealed class DeleteStudentHandler(INotificator notificator,
                                         IStudentRepository studentRepository)
                                       : CommandHandler<DeleteStudentCommand, DeleteStudentResponse>(notificator)
{
    private readonly IStudentRepository _studentRepository = studentRepository;

    public override async Task<Response<DeleteStudentResponse>> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
    {
        var student = await _studentRepository.GetByIdAsync(request.StudentId);
        if (student is null)
        {
            Notify("Student not found.");
            return Response<DeleteStudentResponse>.Failure(GetNotifications(), code: 404);
        }

        await _studentRepository.DeleteAsync(request.StudentId);

        return Response<DeleteStudentResponse>.Success(null, 204);
    }
}