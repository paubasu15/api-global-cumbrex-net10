using CumbrexSaaS.Domain.Common;

namespace CumbrexSaaS.Domain.Entities.Platform;

/// <summary>Represents a platform-level super-admin (system owner).</summary>
public class SystemOwner : BaseEntity
{
    /// <summary>Gets or sets the user identifier associated with the system owner.</summary>
    public Guid UserId { get; set; }

    /// <summary>Gets or sets a value indicating whether the system owner account is active.</summary>
    public bool IsActive { get; set; } = true;
}
