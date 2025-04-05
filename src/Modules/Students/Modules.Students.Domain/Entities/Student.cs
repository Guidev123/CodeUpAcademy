using CodeUp.SharedKernel.DomainObjects;
using CodeUp.SharedKernel.ValueObjects;
using Modules.Students.Domain.Enums;

namespace Modules.Students.Domain.Entities;

public class Student : Entity, IAggregateRoot
{
    public Student(Guid id, string firstName, string lastName, string email, string phone, string document, DateTime birthDate)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = new Email(email);
        Phone = new Phone(phone);
        Document = new Document(document);
        BirthDate = birthDate;
        Type = StudentTypeEnum.Free;
        Validate();
    }

    protected Student()
    { }

    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public Email Email { get; private set; } = null!;
    public Phone Phone { get; private set; } = null!;
    public Document Document { get; private set; } = null!;
    public DateTime BirthDate { get; private set; }
    public string? ProfilePicture { get; private set; }
    public StudentTypeEnum Type { get; private set; }
    public Address? Address { get; private set; }

    public void SetProfilePicture(string picture)
    {
        AssertionConcern.EnsureNotEmpty(picture, "Picture cannot be empty.");
        ProfilePicture = picture;
    }

    public override void Validate()
    {
        AssertionConcern.EnsureNotEmpty(FirstName, "First name cannot be empty.");
        AssertionConcern.EnsureNotEmpty(LastName, "Last name cannot be empty.");
        AssertionConcern.EnsureNotNull(Email, "Email cannot be null.");
        AssertionConcern.EnsureNotNull(Phone, "Phone cannot be null.");
        AssertionConcern.EnsureNotNull(Document, "Document cannot be null.");
        AssertionConcern.EnsureLengthInRange(Document.Number, 11, 11, "Document is invalid.");
    }
}