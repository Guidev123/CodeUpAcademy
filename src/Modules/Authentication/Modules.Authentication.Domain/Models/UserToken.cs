namespace Modules.Authentication.Domain.Models;

public class UserToken
{
    public UserToken(Guid userId, string token, string userEmail)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Token = token;
        UserEmail = userEmail;
        ExpiresAt = DateTime.Now.AddMinutes(8);
    }
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public DateTime ExpiresAt { get; private set; }
    public string UserEmail { get; private set; } = string.Empty;
    public string Token { get; private set; } = string.Empty;
}
