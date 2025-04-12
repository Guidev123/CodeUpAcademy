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
        Validate();
    }

    protected Address()
    { }

    public string Street { get; private set; } = string.Empty;
    public string Number { get; private set; } = string.Empty;
    public string AdditionalInfo { get; private set; } = string.Empty;
    public string Neighborhood { get; private set; } = string.Empty;
    public string ZipCode { get; private set; } = string.Empty;
    public string City { get; private set; } = string.Empty;
    public string State { get; private set; } = string.Empty;
    public Guid StudentId { get; private set; }
    public Student Student { get; private set; } = null!;

    public override void Validate()
    {
        AssertionConcern.EnsureNotEmpty(Street, "Street cannot be empty.");
        AssertionConcern.EnsureLengthInRange(Street, 2, 100, "Street must be between 2 and 100 characters.");

        AssertionConcern.EnsureNotEmpty(Number, "Number cannot be empty.");
        AssertionConcern.EnsureLengthInRange(Number, 1, 10, "Number must be between 1 and 10 characters.");

        AssertionConcern.EnsureNotEmpty(Neighborhood, "Neighborhood cannot be empty.");
        AssertionConcern.EnsureLengthInRange(Neighborhood, 2, 50, "Neighborhood must be between 2 and 50 characters.");

        AssertionConcern.EnsureNotEmpty(ZipCode, "ZipCode cannot be empty.");

        AssertionConcern.EnsureNotEmpty(City, "City cannot be empty.");
        AssertionConcern.EnsureLengthInRange(City, 2, 50, "City must be between 2 and 50 characters.");

        AssertionConcern.EnsureNotEmpty(State, "State cannot be empty.");
        AssertionConcern.EnsureLengthInRange(State, 2, 50, "State must be between 2 and 50 characters.");

        AssertionConcern.EnsureNotNull(StudentId, "StudentId cannot be null.");
    }
}