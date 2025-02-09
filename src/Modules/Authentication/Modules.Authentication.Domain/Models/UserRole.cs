namespace Modules.Authentication.Domain.Models;

public class UserRole
{
    public UserRole(long roleId, Guid userId)
    {
        RoleId = roleId;
        UserId = userId;
    }

    public long RoleId { get; private set; }
    public Guid UserId { get; private set; }
    public User User { get; private set; } = null!;
    public Role Role { get; private set; } = null!;
}
