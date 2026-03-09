using CumbrexSaaS.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CumbrexSaaS.Infrastructure.Persistence.Configurations.Identity;

/// <summary>EF Core configuration for the <see cref="UserTenantRole"/> entity.</summary>
public sealed class UserTenantRoleConfiguration : BaseEntityConfiguration<UserTenantRole>
{
    /// <inheritdoc />
    protected override void ConfigureEntity(EntityTypeBuilder<UserTenantRole> builder)
    {
        builder.ToTable("TBL_USER_TENANT_ROLES");

        builder.Property(e => e.UserTenantId)
            .HasColumnName("USER_TENANT_ID")
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

        builder.Property(e => e.RoleId)
            .HasColumnName("ROLE_ID")
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

        builder.HasIndex(e => new { e.UserTenantId, e.RoleId })
            .IsUnique()
            .HasDatabaseName("UQ_TBL_USER_TENANT_ROLES_USERTENANT_ROLE");

        builder.HasOne(e => e.UserTenant)
            .WithMany(ut => ut.UserTenantRoles)
            .HasForeignKey(e => e.UserTenantId)
            .HasConstraintName("FK_TBL_USER_TENANT_ROLES_USER_TENANT_ID")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.Role)
            .WithMany(r => r.UserTenantRoles)
            .HasForeignKey(e => e.RoleId)
            .HasConstraintName("FK_TBL_USER_TENANT_ROLES_ROLE_ID")
            .OnDelete(DeleteBehavior.Restrict);
    }
}
