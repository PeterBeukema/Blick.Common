namespace Blick.Common.Security.Abstractions;

public interface IHasher
{
    public (string Hash, string Salt) Hash(string value);

    public bool Matches(string value, string hash, string salt);
}