using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Blick.Common.Security.Abstractions;

namespace Blick.Common.Security;

public class Hasher : IHasher
{
    private readonly IEncryptor encryptor;
    
    private const int SaltSize = 32;
    private const int HashSize = 32;
    private const int Iterations = 10000;

    public Hasher(IEncryptor encryptor)
    {
        this.encryptor = encryptor;
    }

    public (byte[] Hash, byte[] Salt) Hash(string value)
    {
        var salt = new byte[SaltSize];

        using var randomNumberGenerator = RandomNumberGenerator.Create();
        
        randomNumberGenerator.GetBytes(salt);

        using var deriveBytes = new Rfc2898DeriveBytes(value, salt, Iterations, HashAlgorithmName.SHA256);

        var hash = deriveBytes.GetBytes(HashSize);
        
        return (hash, salt);
    }

    public bool AreEqual(string value, byte[] hashedValue, byte[] salt)
    {
        using var deriveBytes = new Rfc2898DeriveBytes(value, salt, Iterations, HashAlgorithmName.SHA256);

        var hashBytes = deriveBytes.GetBytes(HashSize);

        return SlowEquals(hashBytes, hashedValue);
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