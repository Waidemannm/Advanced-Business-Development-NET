using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Text.Json;
using OnlineStore.Infrastructure.Persistence;

namespace OnlineStore.API.Extensions;

public static class HealthCheckExtensions
{
    public static IServiceCollection AddOnlineStoreHealthChecks(this IServiceCollection services)
    {
        services.AddHealthChecks()
            .AddCheck("self", () => HealthCheckResult.Healthy("API is running"))
            .AddDbContextCheck<OnlineStoreContext>("database");

        return services;
    }

    public static IApplicationBuilder UseOnlineStoreHealthChecks(this IApplicationBuilder app, IHostEnvironment env)
    {
        app.UseHealthChecks("/health", new HealthCheckOptions
        {
            ResponseWriter = async (context, report) =>
            {
                context.Response.ContentType = "application/json";

                var response = new
                {
                    status = report.Status.ToString(),
                    duration = report.TotalDuration.ToString(),
                    checks = report.Entries.Select(e => new
                    {
                        name = e.Key,
                        status = e.Value.Status.ToString(),
                        duration = e.Value.Duration.ToString(),
                        description = e.Value.Description,
                        error = env.IsDevelopment() ? e.Value.Exception?.Message : null
                    })
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(response, new JsonSerializerOptions { WriteIndented = true }));
            }
        });

        return app;
    }
}
