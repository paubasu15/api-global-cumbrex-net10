using CumbrexSaaS.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CumbrexSaaS.Infrastructure.Persistence.Configurations;

/// <summary>
/// Base EF Core configuration for all <see cref="BaseEntity"/> types.
/// Applies the TBL_ table prefix, NEWSEQUENTIALID() default for the PK,
/// audit columns, soft-delete columns and a global query filter.
/// </summary>
public abstract class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
    where TEntity : BaseEntity
{
    /// <inheritdoc />
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        // Primary Key
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .HasColumnType("UNIQUEIDENTIFIER")
            .HasDefaultValueSql("NEWSEQUENTIALID()")
            .ValueGeneratedOnAdd();

        // Audit columns
        builder.Property(e => e.CreatedAt)
            .HasColumnType("DATETIME2(7)")
            .HasDefaultValueSql("SYSUTCDATETIME()")
            .IsRequired();

        builder.Property(e => e.CreatedBy)
            .HasColumnType("UNIQUEIDENTIFIER");

        builder.Property(e => e.UpdatedAt)
            .HasColumnType("DATETIME2(7)");

        builder.Property(e => e.UpdatedBy)
            .HasColumnType("UNIQUEIDENTIFIER");

        // Soft-delete columns
        builder.Property(e => e.IsDeleted)
            .HasColumnType("BIT")
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(e => e.DeletedAt)
            .HasColumnType("DATETIME2(7)");

        builder.Property(e => e.DeletedBy)
            .HasColumnType("UNIQUEIDENTIFIER");

        // Global query filter: exclude soft-deleted records
        builder.HasQueryFilter(e => !e.IsDeleted);

        ConfigureEntity(builder);
    }

    /// <summary>
    /// Override this method to add entity-specific configuration in derived classes.
    /// </summary>
    protected abstract void ConfigureEntity(EntityTypeBuilder<TEntity> builder);
}
