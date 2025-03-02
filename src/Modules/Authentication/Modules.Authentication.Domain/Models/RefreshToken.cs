namespace Modules.Authentication.Domain.Models;

public class RefreshToken
{
    public RefreshToken(string userEmail, DateTime expirationDate)
    {
        Id = Guid.NewGuid();
        UserEmail = userEmail;
        Token = Guid.NewGuid();
        ExpirationDate = expirationDate;
    }

    public Guid Id { get; private set; }
    public string UserEmail { get; private set; } = string.Empty;
    public Guid Token { get; private set; }
    public DateTime ExpirationDate { get; private set; }
}