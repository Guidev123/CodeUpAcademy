using CodeUp.Common.Responses;
using Modules.Authentication.Application.DTOs;
using Modules.Authentication.Domain.Models;

namespace Modules.Authentication.Application.Services;

public interface ITokenService
{
    Task<Response<LoginResponseDTO>> GenerateJWT(string email);

    Task<Response<RefreshToken>> GetRefreshTokenAsync(Guid token);
}