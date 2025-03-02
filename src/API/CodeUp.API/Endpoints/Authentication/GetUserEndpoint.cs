using CodeUp.Common.Responses;
using MediatR;
using Modules.Authentication.Application.Queries.GetById;

namespace CodeUp.API.Endpoints.Authentication;

public sealed class GetUserEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandleAsync).Produces<Response<GetUserByIdResponse>>().RequireAuthorization();

    private static async Task<IResult> HandleAsync(IMediator mediator)
    {
        var result = await mediator.Send(new GetUserByIdQuery());
        return result.IsSuccess ? TypedResults.Ok(result) : TypedResults.BadRequest(result);
    }
}