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

    private readonly List<Event> _notifications = [];
    public IReadOnlyCollection<Event> Notifications => _notifications.AsReadOnly();
    public void AddEvent(Event @event) => _notifications.Add(@event);
    public void RemoveEvent(Event @event) => _notifications.Remove(@event);
    public void ClearEvents() => _notifications.Clear();

    public override bool Equals(object? obj) =>
        obj is Entity entity &&
               Id.Equals(entity.Id) &&
               CreatedAt == entity.CreatedAt &&
               EqualityComparer<List<Event>>.Default.Equals(_notifications, entity._notifications) &&
               EqualityComparer<IReadOnlyCollection<Event>>.Default.Equals(Notifications, entity.Notifications);

    public override int GetHashCode() => HashCode.Combine(Id, CreatedAt, _notifications, Notifications);
}
