using CumbrexSaaS.Domain.Entities.Platform;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CumbrexSaaS.Infrastructure.Persistence.Configurations.Platform;

/// <summary>EF Core configuration for the <see cref="PlanFeature"/> entity.</summary>
public sealed class PlanFeatureConfiguration : BaseEntityConfiguration<PlanFeature>
{
    /// <inheritdoc />
    protected override void ConfigureEntity(EntityTypeBuilder<PlanFeature> builder)
    {
        builder.ToTable("TBL_PLAN_FEATURES");

        builder.Property(e => e.PlanId)
            .HasColumnName("PLAN_ID")
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

        builder.Property(e => e.FeatureKey)
            .HasColumnName("FEATURE_KEY")
            .HasColumnType("NVARCHAR(100)")
            .IsRequired();

        builder.Property(e => e.FeatureName)
            .HasColumnName("FEATURE_NAME")
            .HasColumnType("NVARCHAR(200)")
            .IsRequired();

        builder.Property(e => e.LimitValue)
            .HasColumnName("LIMIT_VALUE")
            .HasColumnType("INT");

        builder.Property(e => e.IsEnabled)
            .HasColumnName("IS_ENABLED")
            .HasColumnType("BIT")
            .HasDefaultValue(true)
            .IsRequired();

        builder.HasIndex(e => new { e.PlanId, e.FeatureKey })
            .IsUnique()
            .HasDatabaseName("UQ_TBL_PLAN_FEATURES_PLAN_FEATURE");

        builder.HasOne(e => e.Plan)
            .WithMany(p => p.Features)
            .HasForeignKey(e => e.PlanId)
            .HasConstraintName("FK_TBL_PLAN_FEATURES_PLAN_ID")
            .OnDelete(DeleteBehavior.Cascade);
    }
}
