using CodeUp.Common.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Modules.Authentication.Application.Commands.ForgotPassword;
using Modules.Authentication.Application.Commands.Login;
using Modules.Authentication.Application.DTOs;

namespace CodeUp.API.Endpoints.Authentication;

public sealed class ForgotPasswordEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/forgot-password", HandleAsync).Produces<Response<ForgotPasswordResponse>>();

    private static async Task<IResult> HandleAsync(IMediator mediator, ForgotPasswordCommand command)
    {
        var result = await mediator.Send(command);
        return result.IsSuccess ? TypedResults.Ok(result) : TypedResults.BadRequest(result);
    }
}
