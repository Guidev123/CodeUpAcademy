using CodeUp.Common.Responses;
using MediatR;
using Modules.Students.Application.Commands.AddProfilePicture;

namespace CodeUp.API.Endpoints.Students;

public sealed class AddProfilePictureEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/profile-picture", HandleAsync).Produces<Response<AddProfilePictureResponse>>();

    private static async Task<IResult> HandleAsync(IMediator mediator, AddProfilePictureCommand command)
    {
        var result = await mediator.Send(command);
        return result.IsSuccess ? TypedResults.NoContent() : TypedResults.BadRequest(result);
    }
}