using CodeUp.SharedKernel.DomainObjects;
using CodeUp.SharedKernel.ValueObjects;

namespace Modules.Authentication.Domain.Models;

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
        Validate();
    }

    protected User()
    { }

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

    public bool UserIsAbleToLogin() =>
        !IsLockedOut || HandleLockedOut();

    private bool HandleLockedOut()
    {
        var lockoutEnd = LockoutEnd <= DateTime.Now;
        if (lockoutEnd)
        {
            ResetAccessFailedCount();
            return true;
        }

        return false;
    }

    private void ResetAccessFailedCount()
    {
        IsLockedOut = false;
        AccessFailedCount = 0;
    }

    public void AddAccessFailedCount()
    {
        if (AccessFailedCount >= 5)
        {
            BlockAccount();
            return;
        }

        AccessFailedCount++;
        LastLogin = DateTime.Now;
    }

    private void BlockAccount()
    {
        LockoutEnd = DateTime.Now.AddMinutes(10);
        LastLogin = DateTime.Now;
        IsLockedOut = true;
    }

    public void RegisterLogin() => LastLogin = DateTime.Now;

    public static UserRole AddRole(Guid userId, Guid roleId) => new(roleId, userId);

    public void UpdatePassword(string password) => PasswordHash = password;

    public void Validate()
    {
        AssertionConcern.EnsureNotEmpty(FirstName, "First name cannot be empty.");
        AssertionConcern.EnsureLengthInRange(FirstName, 2, 50, "First name must be between 2 and 50 characters.");

        AssertionConcern.EnsureNotEmpty(LastName, "Last name cannot be empty.");
        AssertionConcern.EnsureLengthInRange(LastName, 2, 50, "Last name must be between 2 and 50 characters.");

        AssertionConcern.EnsureNotNull(Email, "Email cannot be null.");

        AssertionConcern.EnsureNotNull(Phone, "Phone cannot be null.");

        AssertionConcern.EnsureTrue(BirthDate <= DateTime.Today.AddYears(-16), "User must be at least 16 years old.");

        AssertionConcern.EnsureNotEmpty(PasswordHash, "Password cannot be empty.");
        AssertionConcern.EnsureLengthInRange(PasswordHash, 8, 100, "Password must be between 8 and 100 characters.");
    }
}