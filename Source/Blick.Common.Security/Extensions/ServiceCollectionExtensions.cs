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

        var passwordHasherOptionsSection = configuration.GetSection(PasswordHasherOptions.ConfigurationSectionName);
        
        serviceCollection.Configure<PasswordHasherOptions>(passwordHasherOptionsSection);
        
        return serviceCollection;
    }
}