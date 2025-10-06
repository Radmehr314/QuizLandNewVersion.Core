using Konscious.Security.Cryptography;
using System.Security.Cryptography;
using System.Text;

namespace QuizLand.Application.Contract.Framework;

public static class HashMaker
{
   
    private const int SaltSize = 16;              // bytes
    private const int HashSize = 32;              // bytes (256-bit)
    private const int Iterations = 3;
    private const int MemorySizeKb = 65_536;      // 64 MB
    private static readonly int Parallelism = Math.Min(Environment.ProcessorCount, 4);


    public static (string Hash, string Salt) HashPassword(string password, string pepper)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
        byte[] pwd  = Encoding.UTF8.GetBytes(pepper + password);
        try
        {
            var argon = new Argon2id(pwd)
            {
                Salt = salt,
                DegreeOfParallelism = Parallelism,
                MemorySize = MemorySizeKb,
                Iterations = Iterations
            };

            byte[] hashBytes = argon.GetBytes(HashSize);
            try
            {
                return (Convert.ToBase64String(hashBytes), Convert.ToBase64String(salt));
            }
            finally
            {
                CryptographicOperations.ZeroMemory(hashBytes);
            }
        }
        finally
        {
            CryptographicOperations.ZeroMemory(pwd);
            CryptographicOperations.ZeroMemory(salt);
        }
    }


    public static bool Verify(string inputPassword, string pepper, string saltBase64, string hashBase64)
    {
        byte[] salt = Convert.FromBase64String(saltBase64);
        byte[] pwd  = Encoding.UTF8.GetBytes(pepper + inputPassword);
        try
        {
            var argon = new Argon2id(pwd)
            {
                Salt = salt,
                DegreeOfParallelism = Parallelism,
                MemorySize = MemorySizeKb,
                Iterations = Iterations
            };

            byte[] computed = argon.GetBytes(HashSize);
            try
            {
                return CryptographicOperations.FixedTimeEquals(
                    computed, Convert.FromBase64String(hashBase64));
            }
            finally
            {
                CryptographicOperations.ZeroMemory(computed);
            }
        }
        finally
        {
            CryptographicOperations.ZeroMemory(pwd);
            CryptographicOperations.ZeroMemory(salt);
        }
    }
}
