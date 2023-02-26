namespace Blick.Common.Security.Abstractions;

public interface IPasswordHasher
{
    public (byte[] HashedPassword, byte[] EncryptedSalt) Hash(string password);

    public bool AreEqual(string password, byte[] hashedPassword, byte[] encryptedSalt);
}