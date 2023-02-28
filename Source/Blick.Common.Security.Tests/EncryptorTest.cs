using Xunit;

namespace Blick.Common.Security.Tests;

public class EncryptorTest
{
    [Fact]
    public void Encrypt_WithValidValues_ReturnsTheExpectedResult()
    {
        // Arrange
        var value = new byte[] { 1, 2, 3, 4 };
        const string key = "b8344997a6b74bc9901c0bab18406a8e";
        const string initializationVector = "1c048e3edb1b400f";
        var expectedResult = new byte[] { 40, 192, 64, 103, 117, 119, 219, 88, 197, 64, 225, 202, 100, 33, 55, 153 };
        
        var encryptor = new Encryptor();
        
        // Act
        var result = encryptor.Encrypt(value, key, initializationVector);
        
        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedResult, result);
    }
    
    [Fact]
    public void Decrypt_WithValidValues_ReturnsTheExpectedResult()
    {
        // Arrange
        var expectedValue = new byte[] { 1, 2, 3, 4 };
        const string key = "b8344997a6b74bc9901c0bab18406a8e";
        const string initializationVector = "1c048e3edb1b400f";
        var value = new byte[] { 40, 192, 64, 103, 117, 119, 219, 88, 197, 64, 225, 202, 100, 33, 55, 153 };
        
        var encryptor = new Encryptor();
        
        // Act
        var result = encryptor.Decrypt(value, key, initializationVector);
        
        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedValue, result);
    }
}