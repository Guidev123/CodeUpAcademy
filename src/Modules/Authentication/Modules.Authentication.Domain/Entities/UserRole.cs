using CodeUp.SharedKernel.DomainObjects;

namespace Modules.Authentication.Domain.Entities;

public class UserRole : Entity
{
    public UserRole(Guid userId, Guid roleId)
    {
        UserId = userId;
        RoleId = roleId;
    }

    public Guid UserId { get; private set; }
    public Guid RoleId { get; private set; }
}