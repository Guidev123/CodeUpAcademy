using CodeUp.API.Endpoints.Authentication;

namespace CodeUp.API.Endpoints;

public static class Endpoint
{
    public static void MapEndpoints(this WebApplication app)
    {
        var endpoints = app.MapGroup("");

        endpoints.MapGroup("api/v1/auth")
            .WithTags("Authentication")
            .MapEndpoint<ForgotPasswordEndpoint>()
            .MapEndpoint<LoginUserEndpoint>()
            .MapEndpoint<RegisterUserEndpoint>();
    }

    private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app)
                where TEndpoint : IEndpoint
    {
        TEndpoint.Map(app);
        return app;
    }
}
