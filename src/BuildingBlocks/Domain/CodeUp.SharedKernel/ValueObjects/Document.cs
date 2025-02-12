using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CodeUp.SharedKernel.ValueObjects;

public record Document : ValueObject
{
    public string Number { get; } = string.Empty;

    public Document(string number)
    {
        if (!Validate(number)) throw new InvalidDataException("Invalid Document.");
        Number = number;
    }
    protected Document() { }

    public static string JustNumbers(string input) => new([.. input.Where(char.IsDigit)]);

    public static bool Validate(string number)
    {
        if (string.IsNullOrWhiteSpace(number))
            return false;

        number = JustNumbers(number);

        if (number.Length != 11)
            return false;

        if (_invalidDocumentsNumber.Contains(number))
            return false;

        var numbers = number.Select(digit => int.Parse(digit.ToString())).ToArray();

        int sum = 0;
        for (int i = 0; i < 9; i++)
            sum += numbers[i] * (10 - i);

        int result = sum % 11;
        int firstDigit = result < 2 ? 0 : 11 - result;
        if (numbers[9] != firstDigit)
            return false;

        sum = 0;
        for (int i = 0; i < 10; i++)
            sum += numbers[i] * (11 - i);

        result = sum % 11;
        int secondDigit = result < 2 ? 0 : 11 - result;
        if (numbers[10] != secondDigit)
            return false;

        return true;
    }

    private static readonly HashSet<string> _invalidDocumentsNumber = new()
    {
        "00000000000", "11111111111", "22222222222", "33333333333",
        "44444444444", "55555555555", "66666666666", "77777777777",
        "88888888888", "99999999999"
    };

}
