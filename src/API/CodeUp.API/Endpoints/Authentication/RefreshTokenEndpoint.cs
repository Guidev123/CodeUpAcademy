using CodeUp.Common.Responses;
using MediatR;
using Modules.Authentication.Application.Commands.RefreshToken;
using Modules.Authentication.Application.DTOs;

namespace CodeUp.API.Endpoints.Authentication;

public sealed class RefreshTokenEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/refresh-token/{refreshToken}", HandleAsync).Produces<Response<LoginResponseDTO>>().RequireAuthorization();

    private static async Task<IResult> HandleAsync(IMediator mediator, string refreshToken)
    {
        var result = await mediator.Send(new RefreshTokenCommand(refreshToken));
        return result.IsSuccess ? TypedResults.Created(string.Empty, result) : TypedResults.BadRequest(result);
    }
}