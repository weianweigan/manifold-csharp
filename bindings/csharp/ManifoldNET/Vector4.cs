namespace ManifoldNET;

/// <summary>
/// Initializes a new instance of <see cref="Vector3"/>.
/// </summary>
/// <param name="x"></param>
/// <param name="y"></param>
/// <param name="z"></param>
/// <param name="w"></param>
public struct Vector4(float x, float y, float z, float w)
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
    /// Z Value.
    /// </summary>
    public float z = z;

    /// <summary>
    /// W value.
    /// </summary>
    public float w = w;

    /// <inheritdoc/>
    public override readonly string ToString()
    {
        return $"{nameof(Vector4)}:{{x:{x}, y:{y}, z:{z}, w:{w}}}";
    }
}