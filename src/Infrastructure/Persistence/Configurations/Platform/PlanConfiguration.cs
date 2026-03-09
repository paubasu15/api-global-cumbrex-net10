using CumbrexSaaS.Domain.Entities.Platform;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CumbrexSaaS.Infrastructure.Persistence.Configurations.Platform;

/// <summary>EF Core configuration for the <see cref="Plan"/> entity.</summary>
public sealed class PlanConfiguration : BaseEntityConfiguration<Plan>
{
    /// <inheritdoc />
    protected override void ConfigureEntity(EntityTypeBuilder<Plan> builder)
    {
        builder.ToTable("TBL_PLANS");

        builder.Property(e => e.Code)
            .HasColumnName("CODE")
            .HasColumnType("NVARCHAR(50)")
            .IsRequired();

        builder.Property(e => e.Name)
            .HasColumnName("NAME")
            .HasColumnType("NVARCHAR(200)")
            .IsRequired();

        builder.Property(e => e.Description)
            .HasColumnName("DESCRIPTION")
            .HasColumnType("NVARCHAR(1000)");

        builder.Property(e => e.MonthlyPrice)
            .HasColumnName("MONTHLY_PRICE")
            .HasColumnType("DECIMAL(18,2)")
            .IsRequired();

        builder.Property(e => e.AnnualPrice)
            .HasColumnName("ANNUAL_PRICE")
            .HasColumnType("DECIMAL(18,2)")
            .IsRequired();

        builder.Property(e => e.IsPublic)
            .HasColumnName("IS_PUBLIC")
            .HasColumnType("BIT")
            .HasDefaultValue(true)
            .IsRequired();

        builder.Property(e => e.IsActive)
            .HasColumnName("IS_ACTIVE")
            .HasColumnType("BIT")
            .HasDefaultValue(true)
            .IsRequired();

        builder.HasIndex(e => e.Code)
            .IsUnique()
            .HasDatabaseName("UQ_TBL_PLANS_CODE");
    }
}
