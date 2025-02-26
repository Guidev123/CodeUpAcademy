using CodeUp.SharedKernel.DomainObjects;

namespace Modules.Authentication.Domain.Models;

public class Role : Entity
{
    public Role(string name)
    {
        Name = name;
        Validate();
    }

    public string Name { get; private set; } = string.Empty;
    public void Validate() => AssertionConcern.EnsureNotEmpty(Name, "The name cannot be empty.");
}

