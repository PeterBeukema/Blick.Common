using System.Security.Cryptography;
using System.Text;
using Blick.Common.Security.Abstractions;

namespace Blick.Common.Security;

public class Encryptor : IEncryptor
{
    private const string Key = "";
    private const string InitializationVector = "";
    
    public byte[] Encrypt(byte[] value)
    {
        using var algorithm = Aes.Create();

        algorithm.Key = Encoding.UTF8.GetBytes(Key);
        algorithm.IV = Encoding.UTF8.GetBytes(InitializationVector);

        using var encryptor = algorithm.CreateEncryptor(algorithm.Key, algorithm.IV);

        var encryptedSalt = encryptor.TransformFinalBlock(value, 0, value.Length);

        return encryptedSalt;
    }

    public byte[] Decrypt(byte[] value)
    {
        using var algorithm = Aes.Create();

        algorithm.Key = Encoding.UTF8.GetBytes(Key);
        algorithm.IV = Encoding.UTF8.GetBytes(InitializationVector);

        using var decryptor = algorithm.CreateDecryptor(algorithm.Key, algorithm.IV);

        var decryptedSalt = decryptor.TransformFinalBlock(value, 0, value.Length);

        return decryptedSalt;
    }
}