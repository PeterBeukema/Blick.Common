using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blick.Common.Repository.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRepository<TService, TImplementation>(
        this IServiceCollection serviceCollection,
        ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        where TImplementation : Abstractions.Repository<TImplementation>, TService
    {
        serviceCollection.AddDbContext<TService, TImplementation>(ConfigureFor<TImplementation>, serviceLifetime);

        return serviceCollection;
    }

    private static void ConfigureFor<TStore>(IServiceProvider serviceProvider, DbContextOptionsBuilder optionsBuilder)
    {
        var configuration = serviceProvider.GetRequiredService<IConfiguration>();
        var connectionString = configuration.GetConnectionString(typeof(TStore).Name);

        if (string.IsNullOrEmpty(connectionString))
        {
            var message =
                $"Can not configure database context '{typeof(TStore).Name}' " +
                "because no connection string was found.";

            throw new InvalidOperationException(message);
        }

        optionsBuilder.UseSqlite(connectionString);
    }
}