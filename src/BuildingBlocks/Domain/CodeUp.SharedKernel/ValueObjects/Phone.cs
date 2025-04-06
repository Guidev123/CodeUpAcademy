using CodeUp.SharedKernel.DomainObjects;
using System.Text.RegularExpressions;

namespace CodeUp.SharedKernel.ValueObjects;

public record Phone : ValueObject
{
    public string Number { get; }

    public Phone(string number)
    {
        if (string.IsNullOrEmpty(number))
            throw new DomainException("Phone number can not be empty.");

        if (!IsValidPhoneNumber(number))
            throw new DomainException("Your phone number should be in this format: (XX) XXXXX-XXXX.");

        Number = number;
    }

    private static bool IsValidPhoneNumber(string phoneNumber)
    {
        var phoneRegex = new Regex(@"^\(\d{2}\)\s\d{4,5}-\d{4}$");
        return phoneRegex.IsMatch(phoneNumber);
    }
}