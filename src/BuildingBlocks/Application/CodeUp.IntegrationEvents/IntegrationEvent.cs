using CodeUp.SharedKernel.DomainObjects;

namespace CodeUp.IntegrationEvents;

public abstract class IntegrationEvent : Event
{
    public DateTime OccuredAt { get; private set; }
}