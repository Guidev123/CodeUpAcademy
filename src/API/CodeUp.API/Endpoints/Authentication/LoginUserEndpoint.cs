using CodeUp.Common.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Modules.Authentication.Application.Commands.Login;
using Modules.Authentication.Application.DTOs;

namespace CodeUp.API.Endpoints.Authentication;

public class LoginUserEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/login", HandleAsync).Produces<Response<LoginResponseDTO>>();

    private static async Task<IResult> HandleAsync([FromServices] IMediator mediator, LoginUserCommand command)
    {
        var result = await mediator.Send(command);
        return result.IsSuccess ? TypedResults.Ok(result) : TypedResults.BadRequest(result);
    }
}
