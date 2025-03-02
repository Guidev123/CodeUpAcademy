using CodeUp.Common.Abstractions;
using CodeUp.Common.Notifications;
using CodeUp.Common.Responses;
using Modules.Students.Application.Mappers;
using Modules.Students.Domain.Repositories;

namespace Modules.Students.Application.Commands.Create;

public sealed class CreateStudentHandler(INotificator notificator,
                                         IStudentRepository studentRepository)
                                       : CommandHandler<CreateStudentCommand,
                                         CreateStudentResponse>(notificator)
{
    private readonly IStudentRepository _studentRepository = studentRepository;

    public override async Task<Response<CreateStudentResponse>> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
    {
        if (!ExecuteValidation(new CreateStudentValidation(), request))
            return Response<CreateStudentResponse>.Failure(GetNotifications());

        if (await _studentRepository.AlreadyExistsAsync(request.Document))
        {
            Notify("Student already exists.");
            return Response<CreateStudentResponse>.Failure(GetNotifications());
        }

        await _studentRepository.CreateAsync(request.MapToEntity());

        if (!await _studentRepository.SaveChangesAsync())
        {
            Notify("Fail to persist data.");
            return Response<CreateStudentResponse>.Failure(GetNotifications());
        }

        return Response<CreateStudentResponse>.Success(null, 201);
    }
}