using CodeUp.SharedKernel.DomainObjects;

namespace Modules.Subscriptions.Domain.Entities;

public class Subscription : Entity, IAggregateRoot
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }

}