using CodeUp.SharedKernel.DomainObjects;

namespace Modules.Students.Domain.Entities;

public class Address : Entity
{
    public Address(Guid studentId, string street, string number, string additionalInfo,
                   string neighborhood, string zipCode, string city,
                   string state)
    {
        Street = street;
        Number = number;
        AdditionalInfo = additionalInfo;
        Neighborhood = neighborhood;
        ZipCode = zipCode;
        City = city;
        State = state;
        StudentId = studentId;
    }

    public string Street { get; private set; }
    public string Number { get; private set; }
    public string AdditionalInfo { get; private set; }
    public string Neighborhood { get; private set; }
    public string ZipCode { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }
    public Guid StudentId { get; private set; }
    public Student Student { get; private set; } = null!;
}
