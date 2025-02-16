using CodeUp.Common.Responses;
using MediatR;
using Modules.Authentication.Application.Commands.RefreshToken;
using Modules.Authentication.Application.DTOs;

namespace CodeUp.API.Endpoints.Authentication;

public sealed class RefreshTokenEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/refresh-token", HandleAsync).Produces<Response<LoginResponseDTO>>().RequireAuthorization();

    private static async Task<IResult> HandleAsync(IMediator mediator, RefreshTokenCommand command)
    {
        var result = await mediator.Send(command);
        return result.IsSuccess ? TypedResults.Created(string.Empty, result) : TypedResults.BadRequest(result);
    }
}
