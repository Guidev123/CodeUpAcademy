using CodeUp.SharedKernel.DomainObjects;

namespace Modules.Authentication.Domain.Entities;

public class Role : Entity
{
    public Role(string name) => Name = name;
    public string Name { get; private set; } = string.Empty;

    private readonly List<UserClaim> _claims = [];
    public IReadOnlyCollection<UserClaim> Claims => _claims.AsReadOnly();
}

