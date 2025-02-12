namespace CodeUp.IntegrationEvents.Authentication;

public class RegisteredUserIntegrationEvent : IntegrationEvent
{
    public RegisteredUserIntegrationEvent(Guid id, string firstName, string lastName, string email, string phone, string document, string profilePicture, DateTime birthDate, int type)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Phone = phone;
        Document = document;
        ProfilePicture = profilePicture;
        BirthDate = birthDate;
        Type = type;
    }
    public Guid Id { get; private set; }
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string Phone { get; private set; } = string.Empty;
    public string Document { get; private set; } = string.Empty;
    public string ProfilePicture { get; private set; } = string.Empty;
    public int Type { get; private set; }
    public DateTime BirthDate { get; private set; }
}
