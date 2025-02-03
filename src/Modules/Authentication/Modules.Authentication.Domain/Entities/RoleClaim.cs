using CodeUp.SharedKernel.DomainObjects;

namespace Modules.Authentication.Domain.Entities;

public class RoleClaim : Entity
{
    public RoleClaim(Guid roleId, string claimType, string claimValue)
    {
        RoleId = roleId;
        ClaimType = claimType;
        ClaimValue = claimValue;
    }

    public Guid RoleId { get; private set; }
    public string ClaimType { get; private set; }
    public string ClaimValue { get; private set; }
}
