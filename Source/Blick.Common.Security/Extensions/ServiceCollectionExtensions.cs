using Blick.Common.Security.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Blick.Common.Security.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSecurity(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IHasher, Hasher>();
        serviceCollection.AddSingleton<IEncryptor, Encryptor>();
        
        return serviceCollection;
    }
}