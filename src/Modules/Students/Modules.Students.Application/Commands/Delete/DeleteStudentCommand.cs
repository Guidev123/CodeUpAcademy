using CodeUp.Common.Abstractions;

namespace Modules.Students.Application.Commands.Delete;

public record DeleteStudentCommand(Guid StudentId) : Command<DeleteStudentResponse>;