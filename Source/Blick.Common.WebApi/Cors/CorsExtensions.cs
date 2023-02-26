using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Blick.Common.WebApi.Cors;

public static class CorsExtensions
{
    private const string CorsPolicy = nameof(CorsPolicy);
    private const string CorsOrigins = nameof(CorsOrigins);

    public static WebApplication UseCorsPolicy(this WebApplication application)
    {
        application.UseCors(CorsPolicy);
        
        return application;
    }

    public static WebApplicationBuilder AddCorsPolicy(this WebApplicationBuilder builder)
    {
        var corsOrigins = builder.Configuration
            .GetSection(CorsOrigins)
            .Get<string[]>() ?? new string [] { };

        if (builder.Environment.IsDevelopment())
        {
            var editableCorsOrigins = corsOrigins.ToList();
            
            editableCorsOrigins.Add("https://localhost:4200");
            editableCorsOrigins.Add("http://localhost:4200");

            corsOrigins = editableCorsOrigins.ToArray();
        }

        builder.Services.AddCors(options =>
        {
            options.AddPolicy(CorsPolicy, corsPolicyBuilder => ConfigurePolicy(corsPolicyBuilder, corsOrigins));
        });

        return builder;
    }

    private static void ConfigurePolicy(CorsPolicyBuilder builder, string[] corsOrigins)
        => builder
            .WithOrigins(corsOrigins)
            .AllowAnyHeader()
            .AllowCredentials()
            .AllowAnyMethod();
}