using Modules.Students.Application.Commands.Create;
using Modules.Students.Domain.Entities;

namespace Modules.Students.Application.Mappers;

public static class StudentMapper
{
    public static Student MapToEntity(this CreateStudentCommand command) 
        => new(command.StudentId, command.FirstName, command.LastName, command.Email, command.Phone, command.Document, command.BirthDate, command.ProfilePicture);
}
