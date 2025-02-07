using CodeUp.SharedKernel.DomainObjects;

namespace Modules.Authentication.Domain.Entities;

public class UserClaim : Entity
{
    public UserClaim(Guid userId, string claimType, string claimValue, Role role)
    {
        UserId = userId;
        ClaimType = claimType;
        ClaimValue = claimValue;
        Role = role;
    }

    protected UserClaim() { }

    public Guid UserId { get; private set; }
    public string ClaimType { get; private set; } = string.Empty;
    public string ClaimValue { get; private set; } = string.Empty;
    public Guid RoleId { get; private set; }
    public Role Role { get; private set; } = null!;
    public User User { get; private set; } = null!;
}

