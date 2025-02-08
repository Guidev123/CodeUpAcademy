using CodeUp.Common.Responses;

namespace CodeUp.API.Endpoints.Helpers;

public static class ResponseHelper
{
    public static IResult CustomResponse<T>(this Response<T> response) => response.StatusCode switch
    {
        200 => TypedResults.Ok(response),
        400 => TypedResults.BadRequest(response),
        201 => TypedResults.Created(string.Empty, response),
        204 => TypedResults.NoContent(),
        _ => TypedResults.NotFound(response)
    };
}
