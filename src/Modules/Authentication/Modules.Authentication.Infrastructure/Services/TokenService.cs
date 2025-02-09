﻿using CodeUp.Common.Extensions;
using CodeUp.Common.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Modules.Authentication.Application.DTOs;
using Modules.Authentication.Application.Services;
using Modules.Authentication.Domain.Models;
using Modules.Authentication.Domain.Repositories;
using Modules.Authentication.Infrastructure.Persistence;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Modules.Authentication.Infrastructure.Services;

public sealed class TokenService(IUserRepository userRepository,
                                 IOptions<JwtExtension> jwtSettings,
                                 AuthenticationDbContext context)
                               : ITokenService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly JwtExtension _jwtSettings = jwtSettings.Value;
    private readonly AuthenticationDbContext _context = context;

    public async Task<Response<LoginResponseDTO>> GenerateJWT(string email)
    {
        var user = await GetUserByEmail(email);
        if (user is null)
            return Response<LoginResponseDTO>.Failure(["User not found"], "Invalid Operation", 404);

        var userRoles = await GetUserRoles(user.Id);
        if (userRoles is null || userRoles.Count == 0)
            return Response<LoginResponseDTO>.Failure(["User has no roles"], "Invalid Operation", 404);

        var claims = BuildUserClaimsAsync(user, userRoles);
        var tokenString = GenerateToken(claims);

        var refreshTokenResponse = await GenerateRefreshToken(user.Email.Address);
        if (!refreshTokenResponse.IsSuccess || refreshTokenResponse.Data is null)
            return Response<LoginResponseDTO>.Failure(["Failed to generate refresh token"], "Invalid Operation");

        var result = GetTokenResponse(tokenString, user, claims, refreshTokenResponse.Data);
        return Response<LoginResponseDTO>.Success(result, 201);
    }

    public async Task<Response<RefreshToken>> GetRefreshTokenAsync(Guid refreshToken)
    {
        var token = await _context.RefreshTokens.AsNoTracking().FirstOrDefaultAsync(rt => rt.Token == refreshToken);

        return token is not null && token.ExpirationDate > DateTime.UtcNow
            ? Response<RefreshToken>.Success(token)
            : Response<RefreshToken>.Failure(["Invalid or expired refresh token"], "Invalid Operation");
    }

    #region Helpers
    private async Task<User?> GetUserByEmail(string email) => await _userRepository.GetByEmailAsync(email);

    private async Task<ICollection<string>> GetUserRoles(Guid userId) => await _userRepository.GetUserRolesAsync(userId);

    private static List<Claim> BuildUserClaimsAsync(User user, ICollection<string> userRoles)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email.Address),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()),
            new(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64)
        };

        claims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));
        return claims;
    }

    private string GenerateToken(List<Claim> claims)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(_jwtSettings.ExpirationHours),
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private LoginResponseDTO GetTokenResponse(string encodedToken, User user, IEnumerable<Claim> claims, RefreshToken refreshToken)
        => new(encodedToken, refreshToken.Token,
           new UserTokenDTO(user.Id, user.Email.Address,
           claims.Select(c => new ClaimDTO { Type = c.Type, Value = c.Value })),
           TimeSpan.FromHours(_jwtSettings.ExpirationHours).TotalSeconds);

    private async Task<Response<RefreshToken>> GenerateRefreshToken(string email)
    {
        var refreshToken = new RefreshToken(email, DateTime.UtcNow.AddHours(_jwtSettings.RefreshTokenExpirationInHours));

        _context.RefreshTokens.RemoveRange(_context.RefreshTokens.Where(rt => rt.UserEmail == email));
        await _context.RefreshTokens.AddAsync(refreshToken);
        await _context.SaveChangesAsync();

        return Response<RefreshToken>.Success(refreshToken, 200);
    }

    private static long ToUnixEpochDate(DateTime date)
        => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
    #endregion
}
