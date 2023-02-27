using System;
using Blick.Common.Security.Abstractions;
using Blick.Common.Security.Options;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace Blick.Common.Security.Tests;

public class PasswordHasherTests : IDisposable
{
    private readonly Mock<IEncryptor> encryptorMock;
    private readonly Mock<IHasher> hasherMock;
    private readonly Mock<IOptions<PasswordHasherOptions>> optionsMock;

    public PasswordHasherTests()
    {
        encryptorMock = new Mock<IEncryptor>(MockBehavior.Strict);
        hasherMock = new Mock<IHasher>(MockBehavior.Strict);
        optionsMock = new Mock<IOptions<PasswordHasherOptions>>(MockBehavior.Strict);
    }

    [Fact]
    public void PasswordHasher_WithValidInput_ReturnsTheExpectedOutput()
    {
        // Arrange
        var key = Guid.NewGuid().ToString();
        var initializationVector = Guid.NewGuid().ToString();
        var password = Guid.NewGuid().ToString();
        
        var passwordHasher = BuildPasswordHasher(options => { options.Key = key; options.InitializationVector = initializationVector; });

        var hash = new byte[] { 1, 2, 3 };
        var salt = new byte[] { 3, 2, 1 };
        var encryptedSalt = new byte[] { 6, 5, 4 };
        
        hasherMock
            .Setup(mock => mock.Hash(password))
            .Returns((hash, salt))
            .Verifiable();
        
        encryptorMock
            .Setup(mock => mock.Encrypt(salt, key, initializationVector))
            .Returns(encryptedSalt)
            .Verifiable();
        
        // Act
        var result = passwordHasher.Hash(password);
        
        // Assert
        Assert.Equal(hash, result.HashedPassword);
        Assert.Equal(encryptedSalt, result.EncryptedSalt);
    }

    private PasswordHasher BuildPasswordHasher(Action<PasswordHasherOptions>? configureOptions = null)
    {
        var options = new PasswordHasherOptions();
        
        configureOptions?.Invoke(options);
        
        optionsMock
            .SetupGet(mock => mock.Value)
            .Returns(options)
            .Verifiable();
        
        return new PasswordHasher(
            encryptorMock.Object,
            hasherMock.Object,
            optionsMock.Object);
    }

    public void Dispose()
    {
        encryptorMock.VerifyAll();
        hasherMock.VerifyAll();
        optionsMock.VerifyAll();
    }
}