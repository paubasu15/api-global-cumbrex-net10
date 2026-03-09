using CumbrexSaaS.Domain.Common;

namespace CumbrexSaaS.Domain.Entities.Platform;

/// <summary>Represents a subscription plan available to tenants.</summary>
public class Plan : BaseEntity
{
    /// <summary>Gets or sets the unique code for the plan.</summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>Gets or sets the display name of the plan.</summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>Gets or sets the plan description.</summary>
    public string? Description { get; set; }

    /// <summary>Gets or sets the monthly price of the plan.</summary>
    public decimal MonthlyPrice { get; set; }

    /// <summary>Gets or sets the annual price of the plan.</summary>
    public decimal AnnualPrice { get; set; }

    /// <summary>Gets or sets a value indicating whether this plan is publicly available.</summary>
    public bool IsPublic { get; set; } = true;

    /// <summary>Gets or sets a value indicating whether this plan is active.</summary>
    public bool IsActive { get; set; } = true;

    /// <summary>Gets or sets the features included in this plan.</summary>
    public ICollection<PlanFeature> Features { get; set; } = new List<PlanFeature>();

    /// <summary>Gets or sets the tenants subscribed to this plan.</summary>
    public ICollection<Tenant> Tenants { get; set; } = new List<Tenant>();
}
