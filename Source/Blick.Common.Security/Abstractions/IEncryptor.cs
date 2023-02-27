namespace Blick.Common.Security.Abstractions;

public interface IEncryptor
{
    public byte[] Encrypt(byte[] value, string key, string initializationVector);

    public byte[] Decrypt(byte[] value, string key, string initializationVector);
}