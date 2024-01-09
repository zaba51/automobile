using System;
using System.Security.Cryptography;

public class PasswordHasher
{
    private const int Iterations = 10000;
    
    private const int SaltSize = 16;

    private const int HashSize = 20;

    public static string HashPassword(string password = "")
    {
        byte[] salt;
        new RNGCryptoServiceProvider().GetBytes(salt = new byte[SaltSize]);

        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations);

        byte[] hash = pbkdf2.GetBytes(HashSize);

        byte[] hashWithSalt = new byte[SaltSize + HashSize];
        Array.Copy(salt, 0, hashWithSalt, 0, SaltSize);
        Array.Copy(hash, 0, hashWithSalt, SaltSize, HashSize);

        string hashedPassword = Convert.ToBase64String(hashWithSalt);

        return hashedPassword;
    }

    public static bool VerifyPassword(string password, string hashedPassword)
    {
        byte[] hashWithSalt = Convert.FromBase64String(hashedPassword);

        byte[] salt = new byte[SaltSize];
        Array.Copy(hashWithSalt, 0, salt, 0, SaltSize);

        byte[] hash = new byte[HashSize];
        Array.Copy(hashWithSalt, SaltSize, hash, 0, HashSize);

        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations);

        byte[] computedHash = pbkdf2.GetBytes(HashSize);

        for (int i = 0; i < HashSize; i++)
        {
            if (computedHash[i] != hash[i])
                return false;
        }

        return true;
    }
}