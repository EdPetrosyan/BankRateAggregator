using Konscious.Security.Cryptography;
using System.Security.Cryptography;
using System.Text;

namespace BankRateAggregator.Application.Security;

public static class PasswordHasher
{
    public static string CreatePassword(string password)
    {
        var salt = CreateSalt();
        var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
        {
            Salt = salt,
            Iterations = 8,
            DegreeOfParallelism = 8,
            MemorySize = 32768
        };
        byte[] hash = argon2.GetBytes(32);
        byte[] hashBytes = new byte[16 + 32];
        Buffer.BlockCopy(salt, 0, hashBytes, 0, 16);
        Buffer.BlockCopy(hash, 0, hashBytes, 16, 32);

        string hashString = Convert.ToBase64String(hashBytes);
        return hashString;
    }

    private static byte[] CreateSalt()
    {
        byte[] salt = new byte[16];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }
        return salt;
    }

    public static bool VerifyPassword(string password, string hashedPassword)
    {
        byte[] hashBytes = Convert.FromBase64String(hashedPassword);

        byte[] salt = new byte[16];
        Buffer.BlockCopy(hashBytes, 0, salt, 0, 16);
        byte[] hash = new byte[32];
        Buffer.BlockCopy(hashBytes, 16, hash, 0, 32);

        var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
        {
            Salt = salt,
            Iterations = 8,
            DegreeOfParallelism = 8,
            MemorySize = 32768
        };

        byte[] testHash = argon2.GetBytes(32);
        return hash.SequenceEqual(testHash);
    }
}
