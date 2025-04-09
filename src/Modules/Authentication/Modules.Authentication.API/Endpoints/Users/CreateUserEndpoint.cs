
namespace Modules.Authentication.API.Endpoints.Users
{
    public sealed class CreateUserEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapPost("/", ExecuteAsync);

        private static async Task<IResult> ExecuteAsync()
        {
            await Task.CompletedTask;
            return TypedResults.Ok();
        }
    }
}
