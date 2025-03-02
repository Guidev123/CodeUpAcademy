namespace Modules.Authentication.Application.Services;

public interface IHasherService
{
    string GenerateToken();

    string HashPassword(string password);

    bool VerifyPassword(string password, string hash);
}