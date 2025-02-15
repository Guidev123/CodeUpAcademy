using Modules.Students.Application.Commands.Create;
using Modules.Students.Application.Queries.GetById;
using Modules.Students.Domain.Entities;
using Modules.Students.Domain.Enums;

namespace Modules.Students.Application.Mappers;

public static class StudentMapper
{
    public static Student MapToEntity(this CreateStudentCommand command) 
        => new(command.StudentId, command.FirstName, command.LastName, command.Email, command.Phone, command.Document, command.BirthDate);

    public static GetStudentByIdResponse MapFromEntity(this Student student)
        => new(student.FirstName, student.LastName, student.Email.Address, 
            student.Phone.Number, student.Document.Number, student.BirthDate, 
            student.ProfilePicture, (int)student.Type == (int)StudentTypeEnum.Free ? nameof(StudentTypeEnum.Free) : nameof(StudentTypeEnum.Premium), student.Address);


}
