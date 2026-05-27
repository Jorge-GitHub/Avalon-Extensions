using System.Text;

namespace Avalon.Base.Extension.Types.StringExtensions;

public static class StringFormatExtensions
{
    public static string? ToDigits(this string? value)
    {
        if (value.IsNotNullOrEmpty())
        {
            StringBuilder digits = new(value.Length);
            foreach (char character in value)
            {
                if (char.IsDigit(character))
                {
                    digits.Append(character);
                }
            }

            return digits.Length > 0
                ? digits.ToString() : string.Empty;
        }

        return value;
    }

    public static string ToPhoneNumber(this string phoneNumber)
    {
        phoneNumber = phoneNumber.ToDigits()!;

        if (phoneNumber.IsNotNullOrEmpty())
        {
            if (phoneNumber.Length == 10)
            {
                return $"({phoneNumber[..3]}) {phoneNumber.Substring(3, 3)}-{phoneNumber.Substring(6, 4)}";
            }

            if (phoneNumber.Length == 11 && phoneNumber[0] == '1')
            {
                return $"+1 ({phoneNumber.Substring(1, 3)}) {phoneNumber.Substring(4, 3)}-{phoneNumber.Substring(7, 4)}";
            }

            if (phoneNumber.Length == 7)
            {
                return $"{phoneNumber[..3]}-{phoneNumber.Substring(3, 4)}";
            }
        }

        return phoneNumber;
    }
}
