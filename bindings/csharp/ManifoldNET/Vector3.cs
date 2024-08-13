namespace ManifoldNET;

/// <summary>
/// Initializes a new instance of <see cref="Vector3"/>.
/// </summary>
/// <param name="x"></param>
/// <param name="y"></param>
/// <param name="z"></param>
public struct Vector3(float x, float y, float z)
{
    /// <summary>
    /// X value.
    /// </summary>
    public float x = x;

    /// <summary>
    /// Y value.
    /// </summary>
    public float y = y;

    /// <summary>
    /// Z value.
    /// </summary>
    public float z = z;

    /// <inheritdoc/>
     public readonly override string ToString() => $"{nameof(Vector3)}:{{x:{x}, y:{y}, z:{z}}}";
}
