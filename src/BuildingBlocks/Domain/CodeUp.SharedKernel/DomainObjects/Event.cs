using MediatR;

namespace CodeUp.SharedKernel.DomainObjects;

public abstract class Event : INotification
{
    protected Event()
    {
        OccurredAt = DateTime.Now;
        MessageType = GetType().Name;
        EventId = Guid.NewGuid();
    }

    public DateTime OccurredAt { get; }
    public string MessageType { get; }
    public Guid EventId { get; }
}
