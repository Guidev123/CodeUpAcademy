namespace Modules.Authentication.Application.Queries.GetById;

public record GetUserByIdResponse(Guid Id, string Email, IReadOnlyDictionary<string, string> Claims);