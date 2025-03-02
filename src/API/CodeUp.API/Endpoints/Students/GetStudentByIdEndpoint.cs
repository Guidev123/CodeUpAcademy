using CodeUp.Common.Responses;
using MediatR;
using Modules.Students.Application.Queries.GetById;

namespace CodeUp.API.Endpoints.Students;

public sealed class GetStudentByIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandleAsync).Produces<Response<GetStudentByIdResponse>>();

    private static async Task<IResult> HandleAsync(IMediator mediator)
    {
        var result = await mediator.Send(new GetStudentByIdQuery());
        return result.IsSuccess ? TypedResults.Ok(result) : TypedResults.BadRequest(result);
    }
}