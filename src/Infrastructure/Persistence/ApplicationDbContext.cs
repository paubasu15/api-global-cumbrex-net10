using CumbrexSaaS.Domain.Entities.Authorization;
using CumbrexSaaS.Domain.Entities.Identity;
using CumbrexSaaS.Domain.Entities.Platform;
using CumbrexSaaS.Domain.Entities.Security;
using CumbrexSaaS.Infrastructure.MultiTenancy;
using CumbrexSaaS.Infrastructure.Persistence.Configurations.Authorization;
using CumbrexSaaS.Infrastructure.Persistence.Configurations.Identity;
using CumbrexSaaS.Infrastructure.Persistence.Configurations.Platform;
using CumbrexSaaS.Infrastructure.Persistence.Configurations.Security;
using Microsoft.EntityFrameworkCore;

namespace CumbrexSaaS.Infrastructure.Persistence;

/// <summary>
/// The main EF Core database context for the CumbrexSaaS application.
/// Applies all entity configurations, the upper-snake-case naming convention,
/// and registers the auditable entity interceptor.
/// </summary>
public class ApplicationDbContext : DbContext
{
    private readonly ITenantContext _tenantContext;

    /// <summary>Initializes a new instance of <see cref="ApplicationDbContext"/>.</summary>
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ITenantContext tenantContext)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    // Platform
    /// <summary>Gets or sets the tenants DbSet.</summary>
    public DbSet<Tenant> Tenants => Set<Tenant>();
    /// <summary>Gets or sets the tenant domains DbSet.</summary>
    public DbSet<TenantDomain> TenantDomains => Set<TenantDomain>();
    /// <summary>Gets or sets the plans DbSet.</summary>
    public DbSet<Plan> Plans => Set<Plan>();
    /// <summary>Gets or sets the plan features DbSet.</summary>
    public DbSet<PlanFeature> PlanFeatures => Set<PlanFeature>();
    /// <summary>Gets or sets the system owners DbSet.</summary>
    public DbSet<SystemOwner> SystemOwners => Set<SystemOwner>();

    // Identity
    /// <summary>Gets or sets the users DbSet.</summary>
    public DbSet<User> Users => Set<User>();
    /// <summary>Gets or sets the user tenants DbSet.</summary>
    public DbSet<UserTenant> UserTenants => Set<UserTenant>();
    /// <summary>Gets or sets the roles DbSet.</summary>
    public DbSet<Role> Roles => Set<Role>();
    /// <summary>Gets or sets the permissions DbSet.</summary>
    public DbSet<Permission> Permissions => Set<Permission>();
    /// <summary>Gets or sets the role permissions DbSet.</summary>
    public DbSet<RolePermission> RolePermissions => Set<RolePermission>();
    /// <summary>Gets or sets the user tenant roles DbSet.</summary>
    public DbSet<UserTenantRole> UserTenantRoles => Set<UserTenantRole>();

    // Authorization
    /// <summary>Gets or sets the pages DbSet.</summary>
    public DbSet<Page> Pages => Set<Page>();
    /// <summary>Gets or sets the page actions DbSet.</summary>
    public DbSet<PageAction> PageActions => Set<PageAction>();
    /// <summary>Gets or sets the role page permissions DbSet.</summary>
    public DbSet<RolePagePermission> RolePagePermissions => Set<RolePagePermission>();

    // Security
    /// <summary>Gets or sets the verification codes DbSet.</summary>
    public DbSet<VerificationCode> VerificationCodes => Set<VerificationCode>();
    /// <summary>Gets or sets the refresh tokens DbSet.</summary>
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
    /// <summary>Gets or sets the user sessions DbSet.</summary>
    public DbSet<UserSession> UserSessions => Set<UserSession>();
    /// <summary>Gets or sets the login attempts DbSet.</summary>
    public DbSet<LoginAttempt> LoginAttempts => Set<LoginAttempt>();
    /// <summary>Gets or sets the audit logs DbSet.</summary>
    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Platform
        modelBuilder.ApplyConfiguration(new TenantConfiguration());
        modelBuilder.ApplyConfiguration(new TenantDomainConfiguration());
        modelBuilder.ApplyConfiguration(new PlanConfiguration());
        modelBuilder.ApplyConfiguration(new PlanFeatureConfiguration());
        modelBuilder.ApplyConfiguration(new SystemOwnerConfiguration());

        // Identity
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new UserTenantConfiguration());
        modelBuilder.ApplyConfiguration(new RoleConfiguration(_tenantContext));
        modelBuilder.ApplyConfiguration(new PermissionConfiguration());
        modelBuilder.ApplyConfiguration(new RolePermissionConfiguration());
        modelBuilder.ApplyConfiguration(new UserTenantRoleConfiguration());

        // Authorization
        modelBuilder.ApplyConfiguration(new PageConfiguration());
        modelBuilder.ApplyConfiguration(new PageActionConfiguration());
        modelBuilder.ApplyConfiguration(new RolePagePermissionConfiguration(_tenantContext));

        // Security
        modelBuilder.ApplyConfiguration(new VerificationCodeConfiguration());
        modelBuilder.ApplyConfiguration(new RefreshTokenConfiguration());
        modelBuilder.ApplyConfiguration(new UserSessionConfiguration());
        modelBuilder.ApplyConfiguration(new LoginAttemptConfiguration());
        modelBuilder.ApplyConfiguration(new AuditLogConfiguration());
    }

}
