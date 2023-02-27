using System;
using Blick.Common.Security.Abstractions;
using Blick.Common.Security.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blick.Common.Security.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSecurity(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddSingleton<IHasher, Hasher>();
        serviceCollection.AddSingleton<IEncryptor, Encryptor>();
        serviceCollection.AddSingleton<IPasswordHasher, PasswordHasher>();
        
        var passwordHasherOptions = configuration
            .GetSection(PasswordHasherOptions.ConfigurationSectionName)
            .Get<PasswordHasherOptions>();

        if (passwordHasherOptions == null)
        {
            const string message = $"Can not register security dependencies, since the '{PasswordHasherOptions.ConfigurationSectionName}' section was not found.";

            throw new InvalidOperationException(message);
        }

        serviceCollection.ConfigureOptions(passwordHasherOptions);
        
        return serviceCollection;
    }
}