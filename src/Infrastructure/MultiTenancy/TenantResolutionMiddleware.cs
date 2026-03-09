using CumbrexSaaS.Infrastructure.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CumbrexSaaS.Infrastructure.MultiTenancy;

/// <summary>
/// ASP.NET Core middleware that resolves the current tenant from the HTTP <c>Host</c> header
/// by looking up the hostname in the TBL_TENANT_DOMAINS table.
/// </summary>
public sealed class TenantResolutionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<TenantResolutionMiddleware> _logger;

    /// <summary>Initializes a new instance of <see cref="TenantResolutionMiddleware"/>.</summary>
    public TenantResolutionMiddleware(RequestDelegate next, ILogger<TenantResolutionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    /// <summary>Processes the HTTP request and resolves the tenant.</summary>
    public async Task InvokeAsync(HttpContext context, ApplicationDbContext dbContext, ITenantContext tenantContext)
    {
        var host = context.Request.Host.Host;

        if (!string.IsNullOrWhiteSpace(host))
        {
            var tenantDomain = await dbContext.TenantDomains
                .AsNoTracking()
                .IgnoreQueryFilters()
                .Where(d => d.Host == host && !d.IsDeleted)
                .Select(d => new { d.TenantId })
                .FirstOrDefaultAsync(context.RequestAborted);

            if (tenantDomain is not null)
            {
                tenantContext.SetTenant(tenantDomain.TenantId);
                _logger.LogDebug("Tenant resolved: {TenantId} for host: {Host}", tenantDomain.TenantId, host);
            }
            else
            {
                _logger.LogDebug("No tenant found for host: {Host}", host);
            }
        }

        await _next(context);
    }
}
