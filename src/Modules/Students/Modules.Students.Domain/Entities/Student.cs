using CodeUp.SharedKernel.DomainObjects;
using CodeUp.SharedKernel.ValueObjects;
using Modules.Students.Domain.Enums;

namespace Modules.Students.Domain.Entities;

public class Student : Entity, IAggregateRoot
{
    public Student(Guid id, string firstName, string lastName, string email, string phone, string document, DateTime birthDate, string profilePicture)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = new Email(email);
        Phone = new Phone(phone);
        Document = new Document(document);
        BirthDate = birthDate;
        ProfilePicture = profilePicture;
        Type = StudentTypeEnum.Free;
    }
    protected Student() { }

    public override Guid Id { get; }
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public Email Email { get; private set; } = null!;
    public Phone Phone { get; private set; } = null!;
    public Document Document { get; private set; } = null!;
    public DateTime BirthDate { get; private set; }
    public string ProfilePicture { get; private set; } = string.Empty;
    public StudentTypeEnum Type { get; private set; }
    public Address? Address { get; private set; }
    public void SetAddress(Address address) => Address = address;   
    public void SetStudentAsPremium() => Type = StudentTypeEnum.Premium;
}
