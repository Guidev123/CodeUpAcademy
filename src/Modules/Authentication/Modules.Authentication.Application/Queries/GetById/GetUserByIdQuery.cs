using CodeUp.Common.Abstractions;

namespace Modules.Authentication.Application.Queries.GetById;

public record GetUserByIdQuery() : IQuery<GetUserByIdResponse>;
