using System.Security.Cryptography;
using System.Text.RegularExpressions;

public static class PublicKeyParser
{
    public static bool TryParseSpki(string input, out byte[] spkiBytes, out string? reason)
    {
        spkiBytes = Array.Empty<byte>();
        reason = null;

        if (string.IsNullOrWhiteSpace(input))
        { reason = "empty"; return false; }

        string s = Normalize(input);

        // A) as-is
        if (TryDecodeAndValidate(s, out spkiBytes, out reason)) { reason = null; return true; }

        // B) احتمال form-urlencoded → space به '+'
        if (s.Contains(' ') && !s.Contains('+'))
        {
            var s2 = s.Replace(' ', '+');
            if (TryDecodeAndValidate(s2, out spkiBytes, out reason)) { reason = null; return true; }
        }

        // C) Base64Url → Base64
        if (s.Contains('-') || s.Contains('_'))
        {
            var s3 = s.Replace('-', '+').Replace('_', '/');
            if (TryDecodeAndValidate(s3, out spkiBytes, out reason)) { reason = null; return true; }
        }

        // D) اگر %2B / %2F / %3D خام آمده
        if (s.Contains('%'))
        {
            try
            {
                var s4 = Uri.UnescapeDataString(s);
                if (TryDecodeAndValidate(s4, out spkiBytes, out reason)) { reason = null; return true; }
            }
            catch { /* ignore */ }
        }

        return false;
    }

    public static bool TryParseSpki(string input, out byte[] spkiBytes)
        => TryParseSpki(input, out spkiBytes, out _);

    private static string Normalize(string input)
    {
        var s = input.Trim().Trim('"');
        s = s.Replace("\u200c","").Replace("\u200e","").Replace("\u200f",""); // zero-width/RTL
        s = s.Replace("-----BEGIN PUBLIC KEY-----", "", StringComparison.OrdinalIgnoreCase)
             .Replace("-----END PUBLIC KEY-----",   "", StringComparison.OrdinalIgnoreCase)
             .Replace("-----BEGINPUBLICKEY-----",   "", StringComparison.OrdinalIgnoreCase)
             .Replace("-----ENDPUBLICKEY-----",     "", StringComparison.OrdinalIgnoreCase);
        s = Regex.Replace(s, @"[\r\n\t]", "");
        return s.Trim();
    }

    private static bool TryDecodeAndValidate(string s, out byte[] spki, out string reason)
    {
        spki = Array.Empty<byte>();
        reason = "decode-failed";

        s = Regex.Replace(s, @"\s+", ""); // حذف white-space
        int mod = s.Length % 4;
        if (mod == 1) { reason = "padding-mod-1"; return false; }
        if (mod == 2) s += "==";
        else if (mod == 3) s += "=";

        byte[] raw;
        try { raw = Convert.FromBase64String(s); }
        catch (FormatException fe) { reason = $"format:{fe.Message}"; return false; }
        catch (Exception ex)       { reason = $"decode-ex:{ex.GetType().Name}"; return false; }

        try
        {
            using var ecdsa = ECDsa.Create();
            ecdsa.ImportSubjectPublicKeyInfo(raw, out _);
            if (ecdsa.KeySize != 256) { reason = $"keysize:{ecdsa.KeySize}"; return false; }
            spki = raw; return true;
        }
        catch (Exception ex) { reason = $"spki-import:{ex.GetType().Name}"; return false; }
    }
    
    public static string StrongNormalize(string input)
    {
        var s = (input ?? string.Empty).Trim().Trim('"');

        // حذف BOM/NBSP/Zero-Width/Bidi control chars
        char[] junk = { '\uFEFF','\u00A0','\u200B','\u200C','\u200D','\u200E','\u200F','\u202A','\u202B','\u202C','\u202D','\u202E' };
        foreach (var j in junk) s = s.Replace(j.ToString(), "");

        // حذف هدر/فوتر PEM
        s = s.Replace("-----BEGIN PUBLIC KEY-----", "", StringComparison.OrdinalIgnoreCase)
            .Replace("-----END PUBLIC KEY-----",   "", StringComparison.OrdinalIgnoreCase);

        // اگر %2B/%2F/%3D خام هست → URL-decode
        if (s.Contains('%'))
        {
            try { s = Uri.UnescapeDataString(s); } catch { /* ignore */ }
        }

        // Base64Url → Base64
        s = s.Replace('-', '+').Replace('_', '/');

        // حذف فاصله‌ها/خطوط
        s = Regex.Replace(s, @"\s+", "");

        // تکمیل padding
        int mod = s.Length % 4;
        if (mod == 1) throw new FormatException("bad length (mod 1)");
        if (mod == 2) s += "==";
        else if (mod == 3) s += "=";

        return s;
    }

    public static (int idx, int code, char ch)? FirstInvalidBase64Char(string input)
    {
        if (string.IsNullOrEmpty(input)) return null;

        const string ok = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=";
        for (int i = 0; i < input.Length; i++)
        {
            var c = input[i];
            if (char.IsWhiteSpace(c)) continue;
            if (!ok.Contains(c)) return (i, (int)c, c);
        }
        return null;
    }
}
