namespace CodeUp.SharedKernel.DomainObjects;

public abstract class Entity
{
    protected Entity()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.Now;
    }

    public Guid Id { get; }
    public DateTime CreatedAt { get; }

    private readonly List<Event> _events = [];
    public IReadOnlyCollection<Event> Events => _events.AsReadOnly();
    public void AddEvent(Event @event) => _events.Add(@event);
    public void RemoveEvent(Event @event) => _events.Remove(@event);
    public void ClearEvents() => _events.Clear();

    public override bool Equals(object? obj) =>
        obj is Entity entity &&
               Id.Equals(entity.Id) &&
               CreatedAt == entity.CreatedAt &&
               EqualityComparer<List<Event>>.Default.Equals(_events, entity._events) &&
               EqualityComparer<IReadOnlyCollection<Event>>.Default.Equals(Events, entity.Events);

    public override int GetHashCode() => HashCode.Combine(Id, CreatedAt, _events, Events);
}
