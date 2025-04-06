using CodeUp.SharedKernel.DomainObjects;
using System.Text.RegularExpressions;

namespace CodeUp.SharedKernel.ValueObjects;

public record Email : ValueObject
{
    public const int ADDRESS_MAX_LENGTH = 254;
    public const int ADDRESS_MIN_LENGTH = 5;
    protected Email() { }
    public Email(string address)
    {
        if (!Validate(address)) throw new DomainException("Invalid Email.");
        Address = address;
    }
    public string Address { get; } = string.Empty;
    public static bool Validate(string address)
    {
        if (string.IsNullOrEmpty(address) || address.Length < ADDRESS_MIN_LENGTH) return false;

        address = address.ToLower().Trim();
        const string pattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

        return Regex.IsMatch(address, pattern);
    }
}