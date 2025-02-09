using CodeUp.SharedKernel.DomainObjects;

namespace CodeUp.IntegrationEvents;

public abstract class IntegrationEvent : Event
{
    public DateOnly OccuredAt { get; private set; }
}
