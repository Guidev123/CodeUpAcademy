using CodeUp.Common.Responses;
using MediatR;
using Modules.Authentication.Application.Commands.Delete;

namespace CodeUp.API.Endpoints.Authentication;

public sealed class DeleteUserEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/{id:guid}", HandleAsync).Produces<Response<DeleteUserResponse>>();

    private static async Task<IResult> HandleAsync(IMediator mediator, Guid id)
    {
        var result = await mediator.Send(new DeleteUserCommand(id));
        return result.IsSuccess ? TypedResults.NoContent() : TypedResults.BadRequest(result);
    }
}
