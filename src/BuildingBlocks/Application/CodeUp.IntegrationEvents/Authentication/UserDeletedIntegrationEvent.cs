namespace CodeUp.IntegrationEvents.Authentication;

public class UserDeletedIntegrationEvent : IntegrationEvent
{
    public UserDeletedIntegrationEvent(Guid userId) => UserId = userId;

    public Guid UserId { get; private set; }
}