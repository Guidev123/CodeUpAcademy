namespace Modules.Authentication.Application.Services;

public interface IUserService
{
    Guid? GetUserId();
    IReadOnlyDictionary<string, string> GetUserClaims();
}
