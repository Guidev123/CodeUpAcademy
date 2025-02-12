namespace CodeUp.SharedKernel.DomainObjects;

public abstract class Entity
{
    protected Entity()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.Now;
        IsDeleted = false;
    }

    public virtual Guid Id { get; }
    public DateTime CreatedAt { get; }
    public bool IsDeleted { get; private set; }
    public DateTime? DeletedAt { get; private set; }
    public void SetAsDeleted()
    {
        DeletedAt = DateTime.Now;   
        IsDeleted = true;
    }

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
