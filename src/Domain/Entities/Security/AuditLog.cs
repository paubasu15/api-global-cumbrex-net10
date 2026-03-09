using CumbrexSaaS.Domain.Common;

namespace CumbrexSaaS.Domain.Entities.Security;

/// <summary>Represents an audit log entry recording a change to a domain entity.</summary>
public class AuditLog : BaseEntity
{
    /// <summary>Gets or sets the tenant identifier (nullable for system-level actions).</summary>
    public Guid? TenantId { get; set; }

    /// <summary>Gets or sets the user identifier who performed the action.</summary>
    public Guid? UserId { get; set; }

    /// <summary>Gets or sets the name of the entity type affected.</summary>
    public string EntityName { get; set; } = string.Empty;

    /// <summary>Gets or sets the identifier of the affected entity.</summary>
    public string EntityId { get; set; } = string.Empty;

    /// <summary>Gets or sets the action performed (e.g., CREATE, UPDATE, DELETE).</summary>
    public string Action { get; set; } = string.Empty;

    /// <summary>Gets or sets the JSON snapshot of the entity before the change.</summary>
    public string? OldValues { get; set; }

    /// <summary>Gets or sets the JSON snapshot of the entity after the change.</summary>
    public string? NewValues { get; set; }

    /// <summary>Gets or sets the IP address of the client that triggered the change.</summary>
    public string? IpAddress { get; set; }
}
