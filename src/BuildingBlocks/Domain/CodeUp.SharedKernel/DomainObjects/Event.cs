using MediatR;

namespace CodeUp.SharedKernel.DomainObjects;

public abstract record Event : INotification
{
    protected Event()
    {
        OccurredAt = DateTime.Now;
        MessageType = GetType().Name;
    }

    public DateTime OccurredAt { get; }
    public string MessageType { get; }
    public Guid AggregateId { get; protected set; }
}