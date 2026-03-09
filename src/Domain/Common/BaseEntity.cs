namespace CumbrexSaaS.Domain.Common;

/// <summary>
/// Base class for all domain entities. Provides audit fields and soft-delete support.
/// </summary>
public abstract class BaseEntity
{
    /// <summary>Gets or sets the unique identifier.</summary>
    public Guid Id { get; set; }

    /// <summary>Gets or sets the UTC datetime when the entity was created.</summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>Gets or sets the user identifier who created the entity.</summary>
    public Guid? CreatedBy { get; set; }

    /// <summary>Gets or sets the UTC datetime when the entity was last updated.</summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>Gets or sets the user identifier who last updated the entity.</summary>
    public Guid? UpdatedBy { get; set; }

    /// <summary>Gets or sets a value indicating whether the entity is soft-deleted.</summary>
    public bool IsDeleted { get; set; }

    /// <summary>Gets or sets the UTC datetime when the entity was soft-deleted.</summary>
    public DateTime? DeletedAt { get; set; }

    /// <summary>Gets or sets the user identifier who soft-deleted the entity.</summary>
    public Guid? DeletedBy { get; set; }
}
