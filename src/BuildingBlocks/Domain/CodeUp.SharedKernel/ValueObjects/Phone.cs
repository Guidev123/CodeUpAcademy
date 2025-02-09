using System.Text.RegularExpressions;

namespace CodeUp.SharedKernel.ValueObjects;

public record Phone
{
    public string Number { get; }

    public Phone(string number)
    {
        if (string.IsNullOrEmpty(number))
            throw new ArgumentException("Phone number can not be empty.", nameof(number));

        if (!IsValidPhoneNumber(number))
            throw new ArgumentException("Your phone number should be in this format: (XX) XXXXX-XXXX.", nameof(number));

        Number = number;
    }

    private static bool IsValidPhoneNumber(string phoneNumber)
    {
        var phoneRegex = new Regex(@"^\(\d{2}\)\s\d{4,5}-\d{4}$");
        return phoneRegex.IsMatch(phoneNumber);
    }
}
