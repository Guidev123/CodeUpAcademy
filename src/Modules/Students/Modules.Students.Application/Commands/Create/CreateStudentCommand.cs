using CodeUp.Common.Abstractions;

namespace Modules.Students.Application.Commands.Create;

public record CreateStudentCommand(Guid StudentId, string FirstName, string LastName,
                                   string Email, string Phone, string Document,
                                   DateTime BirthDate, int Type)
                                 : Command<CreateStudentResponse>;