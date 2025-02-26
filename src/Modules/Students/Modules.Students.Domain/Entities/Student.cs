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

    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public Email Email { get; private set; }
    public Phone Phone { get; private set; }
    public Document Document { get; private set; }
    public DateTime BirthDate { get; private set; }
    public string? ProfilePicture { get; private set; }
    public StudentTypeEnum Type { get; private set; }
    public Address? Address { get; private set; }

    private void Validate()
    {
        AssertionConcern.EnsureNotEmpty(FirstName, "First name cannot be empty.");
        AssertionConcern.EnsureNotEmpty(LastName, "Last name cannot be empty.");
        AssertionConcern.EnsureNotNull(Email, "Email cannot be null.");
        AssertionConcern.EnsureNotNull(Phone, "Phone cannot be null.");
        AssertionConcern.EnsureNotNull(Document, "Document cannot be null.");
    }
}