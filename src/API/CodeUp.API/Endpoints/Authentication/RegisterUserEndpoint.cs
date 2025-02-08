
using CodeUp.Common.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Modules.Authentication.Application.Commands.Register;
using Modules.Authentication.Application.DTOs;

namespace CodeUp.API.Endpoints.Authentication;

public sealed class RegisterUserEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/", HandleAsync).Produces<Response<LoginResponseDTO>>();
    private static async Task<IResult> HandleAsync(IMediator mediator, RegisterUserCommand command)
    {
        var result = await mediator.Send(command);
        return result.IsSuccess ? TypedResults.Created($"/", result) : TypedResults.BadRequest(result);
    }
}
