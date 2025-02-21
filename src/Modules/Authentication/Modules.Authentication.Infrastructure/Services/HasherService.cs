using Modules.Authentication.Application.Services;

namespace Modules.Authentication.Infrastructure.Services;

public sealed class HasherService : IHasherService
{
    public string HashPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password);
    public bool VerifyPassword(string password, string hash) => BCrypt.Net.BCrypt.Verify(password, hash);
    public string GenerateToken() => BCrypt.Net.BCrypt.GenerateSalt();
}
