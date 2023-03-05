using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Blick.Common.Repository.Extensions;

public static class ServiceProviderExtensions
{
    public static IServiceProvider UseRepository<TService, TImplementation>(this IServiceProvider serviceProvider)
        where TImplementation : Abstractions.Repository<TImplementation>, TService
    {
        using var scope = serviceProvider.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<TImplementation>();

        context.Database.EnsureCreated();
        context.Database.Migrate();
        
        context.Initialize();

        return serviceProvider;
    }
}