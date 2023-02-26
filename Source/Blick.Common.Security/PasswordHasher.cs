using Blick.Common.Security.Abstractions;

namespace Blick.Common.Security;

public class PasswordHasher : IPasswordHasher
{
    private readonly IEncryptor encryptor;
    private readonly IHasher hasher;

    public PasswordHasher(IEncryptor encryptor, IHasher hasher)
    {
        this.encryptor = encryptor;
        this.hasher = hasher;
    }
    
    public (byte[] HashedPassword, byte[] EncryptedSalt) Hash(string password)
    {
        var (hashedPassword, salt) = hasher.Hash(password);
        var encryptedSalt = encryptor.Encrypt(salt);

        return (hashedPassword, encryptedSalt);
    }

    public bool AreEqual(string password, byte[] hashedPassword, byte[] encryptedSalt)
    {
        var salt = encryptor.Decrypt(encryptedSalt);
        
        return hasher.AreEqual(password, hashedPassword, salt);
    }
}