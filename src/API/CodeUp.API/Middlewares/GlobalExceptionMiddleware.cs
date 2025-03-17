using System.Net;
using System.Text.Json;

namespace CodeUp.API.Middlewares
{
    public sealed class GlobalExceptionMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
			try
			{
				await next(context);
			}
			catch (Exception ex)
			{
                var problemDetails = new
                {
                    Message = "Invalid Operation.",
                    IsSuccess = false,
                    Errors = new string[] { ex.Message }
                };

                var json = JsonSerializer.Serialize(problemDetails);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync(json);
            }
        }
    }
}
