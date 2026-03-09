namespace CumbrexSaaS.Domain.Enums;

/// <summary>Represents the type of device used in a user session.</summary>
public enum DeviceType
{
    /// <summary>Unknown or undetected device type.</summary>
    Unknown = 0,

    /// <summary>Desktop or laptop computer.</summary>
    Desktop = 1,

    /// <summary>Mobile phone.</summary>
    Mobile = 2,

    /// <summary>Tablet device.</summary>
    Tablet = 3
}
