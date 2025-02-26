using CodeUp.SharedKernel.DomainObjects;

namespace Modules.Authentication.Domain.Models;

public class UserToken : Entity
{
    public UserToken(Guid userId, string token, string userEmail)
    {
        UserId = userId;
        Token = token;
        UserEmail = userEmail;
        ExpiresAt = DateTime.Now.AddMinutes(8);
        Validate();
    }
    public Guid UserId { get; private set; }
    public DateTime ExpiresAt { get; private set; }
    public string UserEmail { get; private set; } = string.Empty;
    public string Token { get; private set; } = string.Empty;

    public void Validate()
    {
        AssertionConcern.EnsureNotEmpty(UserEmail, "User email cannot be empty.");
        AssertionConcern.EnsureMatchesPattern(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", UserEmail, "Invalid email format.");

        AssertionConcern.EnsureNotEmpty(Token, "Token cannot be empty.");
        AssertionConcern.EnsureGreaterThan((ExpiresAt - DateTime.UtcNow).TotalSeconds, 0, "Token expiration must be in the future.");
    }
}
