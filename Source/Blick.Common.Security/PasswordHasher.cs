using Blick.Common.Security.Abstractions;
using Blick.Common.Security.Options;
using Microsoft.Extensions.Options;

namespace Blick.Common.Security;

public class PasswordHasher : IPasswordHasher
{
    private readonly IEncryptor encryptor;
    private readonly IHasher hasher;
    private readonly PasswordHasherOptions options;

    public PasswordHasher(
        IEncryptor encryptor,
        IHasher hasher,
        IOptions<PasswordHasherOptions> options)
    {
        this.encryptor = encryptor;
        this.hasher = hasher;
        this.options = options.Value;
    }
    
    public (byte[] HashedPassword, byte[] EncryptedSalt) Hash(string password)
    {
        var (hashedPassword, salt) = hasher.Hash(password);
        var encryptedSalt = encryptor.Encrypt(salt, options.Key, options.InitializationVector);

        return (hashedPassword, encryptedSalt);
    }

    public bool AreEqual(string password, byte[] hashedPassword, byte[] encryptedSalt)
    {
        var salt = encryptor.Decrypt(encryptedSalt, options.Key, options.InitializationVector);
        
        return hasher.AreEqual(password, hashedPassword, salt);
    }
}