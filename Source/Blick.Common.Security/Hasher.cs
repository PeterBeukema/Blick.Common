using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Blick.Common.Security.Abstractions;

namespace Blick.Common.Security;

public class Hasher : IHasher
{
    private const int SaltSize = 32;
    private const int HashSize = 32;
    private const int Iterations = 10000;

    public (string Hash, string Salt) Hash(string value)
    {
        var saltBytes = new byte[SaltSize];

        using var randomNumberGenerator = RandomNumberGenerator.Create();
        
        randomNumberGenerator.GetBytes(saltBytes);

        using var deriveBytes = new Rfc2898DeriveBytes(value, saltBytes, Iterations, HashAlgorithmName.SHA256);

        var hashBytes = deriveBytes.GetBytes(HashSize);

        var hash = Convert.ToBase64String(hashBytes);
        var salt = Convert.ToBase64String(saltBytes);
        
        return (hash, salt);
    }

    public bool Matches(string value, string hash, string salt)
    {
        var saltBytes = Convert.FromBase64String(salt);

        using var deriveBytes = new Rfc2898DeriveBytes(value, saltBytes, Iterations, HashAlgorithmName.SHA256);

        var hashBytes = deriveBytes.GetBytes(HashSize);
        var hashToCheck = Convert.FromBase64String(hash);

        return SlowEquals(hashBytes, hashToCheck);
    }

    private static bool SlowEquals(IReadOnlyList<byte> hash, IReadOnlyList<byte> hashToCheck)
    {
        var hashLength = (uint)hash.Count;
        var hashToCheckLength = (uint)hashToCheck.Count;
        
        var difference = hashLength ^ hashToCheckLength;

        for (var index = 0; index < hash.Count && index < hashToCheck.Count; index++)
        {
            var hashByte = hash[index];
            var hashToCheckByte = hashToCheck[index];
            
            difference |= (uint)(hashByte ^ hashToCheckByte);
        }

        return difference == 0;
    }
}