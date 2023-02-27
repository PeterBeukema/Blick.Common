using System.Security.Cryptography;
using System.Text;
using Blick.Common.Security.Abstractions;

namespace Blick.Common.Security;

public class Encryptor : IEncryptor
{
    public byte[] Encrypt(byte[] value, string key, string initializationVector)
    {
        using var algorithm = Aes.Create();

        algorithm.Key = Encoding.UTF8.GetBytes(key);
        algorithm.IV = Encoding.UTF8.GetBytes(initializationVector);

        using var encryptor = algorithm.CreateEncryptor(algorithm.Key, algorithm.IV);

        var encryptedSalt = encryptor.TransformFinalBlock(value, 0, value.Length);

        return encryptedSalt;
    }

    public byte[] Decrypt(byte[] value, string key, string initializationVector)
    {
        using var algorithm = Aes.Create();

        algorithm.Key = Encoding.UTF8.GetBytes(key);
        algorithm.IV = Encoding.UTF8.GetBytes(initializationVector);

        using var decryptor = algorithm.CreateDecryptor(algorithm.Key, algorithm.IV);

        var decryptedSalt = decryptor.TransformFinalBlock(value, 0, value.Length);

        return decryptedSalt;
    }
}