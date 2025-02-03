using CodeUp.SharedKernel.DomainObjects;
using Modules.Authentication.Domain.ValueObjects;

namespace Modules.Authentication.Domain.Entities;

public class User : Entity, IAggregateRoot
{
    public User(string firstName, string lastName, string email, string phone, DateTime birthDate, string passwordHash)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = new Email(email);
        Phone = new Phone(phone);
        BirthDate = birthDate;
        PasswordHash = passwordHash;
        IsLockedOut = false;
        AccessFailedCount = 0;
        EmailConfirmed = false;
        PhoneConfirmed = false;
        TwoFactorEnabled = false;
    }
    protected User() { }

    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public Email Email { get; private set; } = null!;
    public Phone Phone { get; private set; } = null!;
    public DateTime BirthDate { get; private set; }
    public DateTime? LastLogin { get; private set; }
    public DateTime? LockoutEnd { get; private set; }
    public string PasswordHash { get; private set; } = string.Empty;
    public int AccessFailedCount { get; private set; }
    public bool IsLockedOut { get; private set; }
    public bool EmailConfirmed { get; private set; }
    public bool PhoneConfirmed { get; private set; }
    public bool TwoFactorEnabled { get; private set; }
}

