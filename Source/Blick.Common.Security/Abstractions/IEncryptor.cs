namespace Blick.Common.Security.Abstractions;

public interface IEncryptor
{
    public byte[] Encrypt(byte[] value);

    public byte[] Decrypt(byte[] value);
}