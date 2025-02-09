using CodeUp.SharedKernel.DomainObjects;

namespace Modules.Authentication.Domain.Entities;

public class UserToken : Entity
{
    public UserToken(Guid userId, string token, string userEmail)
    {
        UserId = userId;
        Token = token;
        UserEmail = userEmail;
        ExpiresAt = DateTime.Now.AddMinutes(8);
    }

    public Guid UserId { get; private set; }
    public DateTime ExpiresAt { get; private set; }
    public string UserEmail { get; private set; } = string.Empty;
    public string Token { get; private set; } = string.Empty;
}
