using Modules.Students.Domain.Entities;

namespace Modules.Students.Application.Queries.GetById;

public record GetStudentByIdResponse(
    string FirstName, string LastName, string Email,
    string Phone, string Document,
    DateTime BirthDate, string? ProfilePicture,
    string Type, Address? Address);