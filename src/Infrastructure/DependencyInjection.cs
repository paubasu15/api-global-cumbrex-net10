using CumbrexSaaS.Infrastructure.MultiTenancy;
using CumbrexSaaS.Infrastructure.Persistence;
using CumbrexSaaS.Infrastructure.Persistence.Interceptors;
using CumbrexSaaS.Infrastructure.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CumbrexSaaS.Infrastructure;

/// <summary>
/// Extension methods for registering Infrastructure layer services with the DI container.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Registers all Infrastructure services: EF Core, multi-tenancy, security, caching and background jobs.
    /// </summary>
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Multi-tenancy
        services.AddScoped<ITenantContext, TenantContext>();

        // Security
        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUserContext, CurrentUserContext>();
        services.AddScoped<AuditableEntityInterceptor>();
        services.AddScoped<CookieTokenService>();
        services.AddScoped<VerificationCodeService>();

        // EF Core
        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            options.UseSqlServer(connectionString, sqlOptions =>
            {
                sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null);
                sqlOptions.CommandTimeout(60);
                sqlOptions.MigrationsHistoryTable("TBL_EF_MIGRATIONS_HISTORY");
            });

            var interceptor = sp.GetRequiredService<AuditableEntityInterceptor>();
            options.AddInterceptors(interceptor);
        });

        return services;
    }
}
