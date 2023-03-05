using Microsoft.AspNetCore.Builder;

namespace Blick.Common.Security.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void AddSecurity(this WebApplicationBuilder builder)
        => builder.Services.AddSecurity(builder.Configuration);
}