using CodeUp.SharedKernel.DomainObjects;
using CodeUp.SharedKernel.ValueObjects;

namespace Modules.Authentication.Domain.Entities;

public class User : Entity, IAggregateRoot
{
    private const int MIN_AGE = 16;

    public User(string firstName, string lastName, string email, string phone, DateTime birthDate)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = new Email(email);
        Phone = new Phone(phone);
        BirthDate = birthDate;
        EmailConfirmed = false;
        PhoneConfirmed = false;
        TwoFactorEnabled = false;
        Validate();
    }

    protected User()
    { }

    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public Email Email { get; private set; } = null!;
    public Phone Phone { get; private set; } = null!;
    public DateTime BirthDate { get; private set; }
    public bool EmailConfirmed { get; private set; }
    public bool PhoneConfirmed { get; private set; }
    public bool TwoFactorEnabled { get; private set; }

    public override void Validate()
    {
        AssertionConcern.EnsureNotEmpty(FirstName, "First name cannot be empty.");
        AssertionConcern.EnsureLengthInRange(FirstName, 2, 50, "First name must be between 2 and 50 characters.");

        AssertionConcern.EnsureNotEmpty(LastName, "Last name cannot be empty.");
        AssertionConcern.EnsureLengthInRange(LastName, 2, 50, "Last name must be between 2 and 50 characters.");

        AssertionConcern.EnsureNotNull(Email, "Email cannot be null.");

        AssertionConcern.EnsureNotNull(Phone, "Phone cannot be null.");

        AssertionConcern.EnsureTrue(BirthDate <= DateTime.Today.AddYears(-MIN_AGE), "User must be at least 16 years old.");
    }
}