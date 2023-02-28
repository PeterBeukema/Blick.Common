using System;
using Xunit;

namespace Blick.Common.Security.Tests;

public class HasherTest
{
    [Fact]
    public void Hash_WithValidInput_ReturnsTheExpectedResult()
    {
        // Arrange
        var value = Guid.NewGuid().ToString();

        var hasher = new Hasher();
    
        // Act
        var result = hasher.Hash(value);
        
        // Assert
        Assert.NotNull(result.Hash);
        Assert.NotNull(result.Salt);
        
        Assert.Equal(32, result.Hash.Length);
        Assert.Equal(32, result.Salt.Length);

        var equalsResult = hasher.AreEqual(value, result.Hash, result.Salt);
        
        Assert.True(equalsResult);
    }

    [Fact]
    public void AreEqual_WithValidValues_ReturnsTrue()
    {
        // Arrange
        const string value = "fa13f756-6daf-4925-bfd8-ed8db5b9f34f";
        var hashedValue = new byte[] { 177, 62, 79, 146, 59, 238, 84, 201, 98, 121, 122, 251, 133, 9, 195, 58, 225, 230, 98, 220, 33, 66, 26, 213, 53, 36, 128, 208, 176, 147, 33, 71 };
        var salt = new byte[] { 242, 93, 216, 178, 86, 217, 121, 179, 173, 232, 52, 71, 87, 120, 213, 125, 240, 112, 160, 76, 29, 58, 247, 253, 116, 220, 211, 45, 120, 44, 37, 51 };
        
        var hasher = new Hasher();
        
        // Act
        var result = hasher.AreEqual(value, hashedValue, salt);
        
        // Assert
        Assert.True(result);
    }

    [Fact]
    public void AreEqual_WithInvalidValues_ReturnsFalse()
    {
        // Arrange
        const string value = "ga13f756-6daf-4925-bfd8-ed8db5b9f34f";
        var hashedValue = new byte[] { 177, 62, 79, 146, 59, 238, 84, 201, 98, 121, 122, 251, 133, 9, 195, 58, 225, 230, 98, 220, 33, 66, 26, 213, 53, 36, 128, 208, 176, 147, 33, 71 };
        var salt = new byte[] { 242, 93, 216, 178, 86, 217, 121, 179, 173, 232, 52, 71, 87, 120, 213, 125, 240, 112, 160, 76, 29, 58, 247, 253, 116, 220, 211, 45, 120, 44, 37, 51 };
        
        var hasher = new Hasher();
        
        // Act
        var result = hasher.AreEqual(value, hashedValue, salt);
        
        // Assert
        Assert.False(result);
    }
}