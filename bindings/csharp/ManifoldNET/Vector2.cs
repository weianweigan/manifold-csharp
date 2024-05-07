namespace ManifoldNET;

/// <summary>
/// Initializes a new instance of <see cref="Vector2"/>.
/// </summary>
/// <param name="x"></param>
/// <param name="y"></param>
public struct Vector2(float x, float y)
{
    /// <summary>
    /// X value.
    /// </summary>
    public float x = x;

    /// <summary>
    /// Y value.
    /// </summary>
    public float y = y;

    /// <inheritdoc/>

    public override readonly string ToString() => 
        $"{nameof(Vector3)}:{{x:{x}, y:{y}}}";
}
