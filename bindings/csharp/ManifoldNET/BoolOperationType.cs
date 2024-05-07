namespace ManifoldNET;

/// <summary>
/// Bool operation options.
/// </summary>
/// <remarks>
/// <list type="number">
/// <item>
/// Contains Add (Union)
/// </item>
/// <item>
/// Subtract (Difference)
/// </item>
/// <item>
/// Intersect.
/// </item>
/// </list>
/// </remarks>
public enum BoolOperationType
{
    /// <summary>
    /// Add(Union).
    /// </summary>
    Add,

    /// <summary>
    /// Subtract(Difference).
    /// </summary>
    Subtract,

    /// <summary>
    /// Intersect.
    /// </summary>
    Intersect,
}