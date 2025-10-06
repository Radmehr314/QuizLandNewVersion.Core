using System.Text.RegularExpressions;

namespace QuizLand.Application.Contract.Framework.Utils;

public static class MaskMobile
{
    public static string MaskMobilePhone(this string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber)) return string.Empty;

        var s = phoneNumber;
        s = Regex.Replace(s, @"\D", ""); // فقط رقم‌ها

        if (Regex.IsMatch(s, @"^09\d{9}$"))
            return $"****-***-{s[^4..]}";

        return "****-***-" + (s.Length >= 4 ? s[^4..] : s.PadLeft(4, '*'));
    }
 
}