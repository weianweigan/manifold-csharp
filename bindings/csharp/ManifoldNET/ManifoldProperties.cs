namespace ManifoldNET;

/// <summary>
/// Manifold properties.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct ManifoldProperties
{
    /// <summary>
    /// Surface area.
    /// </summary>
    public float surface_area;

    /// <summary>
    /// Volume.
    /// </summary>
    public float volume;

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"{{ SurfaceArea:{surface_area}, volume: {volume}}}";
    }
}