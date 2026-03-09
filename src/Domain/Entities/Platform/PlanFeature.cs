using CumbrexSaaS.Domain.Common;

namespace CumbrexSaaS.Domain.Entities.Platform;

/// <summary>Represents a feature included in a subscription plan.</summary>
public class PlanFeature : BaseEntity
{
    /// <summary>Gets or sets the plan identifier.</summary>
    public Guid PlanId { get; set; }

    /// <summary>Gets or sets the feature key identifier.</summary>
    public string FeatureKey { get; set; } = string.Empty;

    /// <summary>Gets or sets the display name of the feature.</summary>
    public string FeatureName { get; set; } = string.Empty;

    /// <summary>Gets or sets the feature limit value (null for unlimited).</summary>
    public int? LimitValue { get; set; }

    /// <summary>Gets or sets a value indicating whether the feature is enabled in this plan.</summary>
    public bool IsEnabled { get; set; } = true;

    /// <summary>Gets or sets the plan navigation property.</summary>
    public Plan? Plan { get; set; }
}
