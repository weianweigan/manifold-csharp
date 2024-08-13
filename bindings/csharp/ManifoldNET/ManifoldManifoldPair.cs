namespace ManifoldNET;

/// <summary>
/// <see cref="Manifold"/> pair.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal struct ManifoldManifoldPair
{
    /// <summary>
    /// First.
    /// </summary>
    public IntPtr First { get; set; }

    /// <summary>
    /// Second.
    /// </summary>
    public IntPtr Second { get; set; }
}