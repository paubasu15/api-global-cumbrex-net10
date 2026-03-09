using System.Text.RegularExpressions;

namespace CumbrexSaaS.Infrastructure.Persistence.Conventions;

/// <summary>
/// Utility class providing UPPER_SNAKE_CASE naming helper used by entity configurations.
/// All entity configurations apply SQL naming conventions explicitly via Fluent API,
/// so no runtime convention is required. This class is kept as a utility reference.
/// </summary>
public static class SqlNamingHelper
{
    /// <summary>Converts a PascalCase name to UPPER_SNAKE_CASE.</summary>
    public static string ToUpperSnakeCase(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) return name;
        var snake = Regex.Replace(name, @"([a-z0-9])([A-Z])", "$1_$2");
        snake = Regex.Replace(snake, @"([A-Z]+)([A-Z][a-z])", "$1_$2");
        return snake.ToUpperInvariant();
    }

    /// <summary>Generates a TBL_ prefixed table name for a given entity type name.</summary>
    public static string GetTableName(string entityName)
    {
        var upperSnake = ToUpperSnakeCase(entityName);
        return $"TBL_{upperSnake}";
    }
}
