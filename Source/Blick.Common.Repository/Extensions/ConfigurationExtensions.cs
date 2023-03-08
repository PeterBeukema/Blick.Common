using System;
using Blick.Common.Repository.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blick.Common.Repository.Extensions;

public static class ConfigurationExtensions
{
    /// <summary>
    /// Registers the provided repository as a service.
    /// </summary>
    /// <param name="builder">The web application builder instance.</param>
    /// <param name="serviceLifetime">The repository's lifetime to register it with.</param>
    /// <typeparam name="TInterface">The interface to register the repository with.</typeparam>
    /// <typeparam name="TImplementation">The repository's implementing instance type.</typeparam>
    /// <returns>The updated web application builder.</returns>
    public static WebApplicationBuilder AddRepository<TInterface, TImplementation>(
        this WebApplicationBuilder builder,
        ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        where TImplementation : Abstractions.Repository<TImplementation>, TInterface
    {
        builder.Services.AddDbContext<TInterface, TImplementation>(ConfigureFor<TImplementation>, serviceLifetime);

        return builder;
    }

    /// <summary>
    /// Configures the provided repository to be used in the application.
    /// </summary>
    /// <param name="application">The web application instance to configure the repository for.</param>
    /// <typeparam name="TImplementation">The repository's instance type.</typeparam>
    /// <returns>The updated web application.</returns>
    public static WebApplication UseRepository<TImplementation>(this WebApplication application)
        where TImplementation : Repository<TImplementation>
    {
        using var scope = application.Services.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<TImplementation>();

        context.Database.EnsureCreated();
        context.Database.Migrate();
        
        context.Initialize();

        return application;
    }

    private static void ConfigureFor<TRepository>(IServiceProvider serviceProvider, DbContextOptionsBuilder optionsBuilder)
    {
        var configuration = serviceProvider.GetRequiredService<IConfiguration>();
        var connectionString = configuration.GetConnectionString(typeof(TRepository).Name);

        if (string.IsNullOrEmpty(connectionString))
        {
            var message =
                $"Can not configure database context '{typeof(TRepository).Name}' " +
                "because no connection string was found.";

            throw new InvalidOperationException(message);
        }

        optionsBuilder.UseSqlite(connectionString);
    }
}