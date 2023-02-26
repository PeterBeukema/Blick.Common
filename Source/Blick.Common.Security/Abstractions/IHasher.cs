namespace Blick.Common.Security.Abstractions;

public interface IHasher
{
    public (byte[] Hash, byte[] Salt) Hash(string value);

    public bool AreEqual(string value, byte[] hashedValue, byte[] salt);
}