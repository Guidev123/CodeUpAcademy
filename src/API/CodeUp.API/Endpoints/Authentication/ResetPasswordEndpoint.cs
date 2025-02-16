using CodeUp.Common.Responses;
using MediatR;
using Modules.Authentication.Application.Commands.ResetPassword;

namespace CodeUp.API.Endpoints.Authentication;

public sealed class ResetPasswordEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    => app.MapPut("/reset-password", HandleAsync).Produces<Response<ResetPasswordResponse>>().RequireAuthorization();

    private static async Task<IResult> HandleAsync(IMediator mediator, ResetPasswordCommand command)
    {
        var result = await mediator.Send(command);
        return result.IsSuccess ? TypedResults.Ok(result) : TypedResults.BadRequest(result);
    }
}
