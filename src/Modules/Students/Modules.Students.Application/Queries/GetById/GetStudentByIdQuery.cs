using CodeUp.Common.Abstractions;

namespace Modules.Students.Application.Queries.GetById;

public record GetStudentByIdQuery() : IQuery<GetStudentByIdResponse>;