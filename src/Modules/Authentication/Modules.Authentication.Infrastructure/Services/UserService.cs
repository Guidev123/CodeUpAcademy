using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Modules.Authentication.Application.Services;
using System.IdentityModel.Tokens.Jwt;

namespace Modules.Authentication.Infrastructure.Services;

public sealed class UserService(IHttpContextAccessor httpContextAccessor) : IUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public HttpContext GetHttpContext() => _httpContextAccessor.HttpContext!;
    private string GetToken()
    {
        if (GetHttpContext().Request.Headers.TryGetValue("Authorization", out StringValues authorizationHeader))
        {
            var token = authorizationHeader.ToString();
            if (token.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                return token["Bearer ".Length..].Trim();
        }

        return string.Empty;
    }

    public Guid? GetUserId()
    {
        var token = GetToken();
        if (string.IsNullOrEmpty(token)) return null;

        var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(token);

        var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
        if (Guid.TryParse(userIdClaim, out var userId)) return userId;

        return null;
    }

    public IReadOnlyDictionary<string, string> GetUserClaims()
    {
        var token = GetToken();
        if (string.IsNullOrEmpty(token)) return new Dictionary<string, string>();

        var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(token);

        return jwtToken.Claims.ToDictionary(c => c.Type, c => c.Value);
    }
}
