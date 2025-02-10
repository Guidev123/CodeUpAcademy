
using CodeUp.API.Endpoints.Helpers;
using CodeUp.Common.Responses;
using MediatR;
using Modules.Authentication.Application.Queries.GetById;

namespace CodeUp.API.Endpoints.Authentication;

public sealed class GetUserEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/manage/info", HandleAsync).Produces<Response<GetUserByIdResponse>>();

    private static async Task<IResult> HandleAsync(IMediator mediator)
    {
        var result = await mediator.Send(new GetUserByIdQuery());
        return ResponseHelper.CustomResponse(result);
    }
}
