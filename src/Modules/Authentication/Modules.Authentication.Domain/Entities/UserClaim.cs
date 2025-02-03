using CodeUp.SharedKernel.DomainObjects;

namespace Modules.Authentication.Domain.Entities;

public class UserClaim : Entity
{
    public UserClaim(Guid userId, string claimType, string claimValue)
    {
        UserId = userId;
        ClaimType = claimType;
        ClaimValue = claimValue;
    }

    public Guid UserId { get; private set; }
    public string ClaimType { get; private set; }
    public string ClaimValue { get; private set; }
}
