﻿using System.Security.Cryptography;
using System.Text;

namespace BackendCas.UTILITY;

public class PasswordHelper
{
    public static byte[] HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            return sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
    }

    public static bool VerifyPassword(string password, byte[] storedHash)
    {
        var hash = HashPassword(password);
        return hash.SequenceEqual(storedHash);
    }
}