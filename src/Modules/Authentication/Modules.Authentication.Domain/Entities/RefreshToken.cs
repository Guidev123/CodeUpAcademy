using CodeUp.SharedKernel.DomainObjects;

namespace Modules.Authentication.Domain.Entities;

public class RefreshToken : Entity
{
    public RefreshToken(string userEmail, DateTime expirationDate)
    {
        UserEmail = userEmail;
        Token = Guid.NewGuid();
        ExpirationDate = expirationDate;
    }

    public string UserEmail { get; private set; } = string.Empty;
    public Guid Token { get; private set; }
    public DateTime ExpirationDate { get; private set; }
}
