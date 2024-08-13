using static ManifoldNET.Import.Native;

namespace ManifoldNET;

/// <summary>
/// Represents a rectangle with minimum and maximum coordinates.
/// </summary>
public sealed class Rect : ManifoldObject
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Rect"/> class with specified minimum and maximum coordinates.
    /// </summary>
    /// <param name="min">The minimum (bottom-left) coordinates of the rectangle.</param>
    /// <param name="max">The maximum (top-right) coordinates of the rectangle.</param>
    public Rect(Vector2 min, Vector2 max)
        : base(
            manifold_rect(
                Marshal.AllocHGlobal((int)manifold_rect_size()),
                min.x,
                min.y,
                max.x,
                max.y
            )
        ) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Rect"/> class with a specified pointer to unmanaged memory.
    /// </summary>
    /// <param name="pointer">A pointer to unmanaged memory representing the rectangle.</param>
    internal Rect(IntPtr pointer)
        : base(pointer) { }

    /// <summary>
    /// Gets the minimum (bottom-left) coordinates of the rectangle.
    /// </summary>
    public Vector2 Min => manifold_rect_min(_pointer);

    /// <summary>
    /// Gets the maximum (top-right) coordinates of the rectangle.
    /// </summary>
    public Vector2 Max => manifold_rect_max(_pointer);

    /// <summary>
    /// Gets the size (width and height) of the rectangle.
    /// </summary>
    public Vector2 Size => manifold_rect_dimensions(_pointer);

    /// <summary>
    /// Gets the center coordinates of the rectangle.
    /// </summary>
    public Vector2 Center => manifold_rect_center(_pointer);

    /// <summary>
    /// Gets the scale factor of the rectangle.
    /// </summary>
    public float Scale => manifold_rect_scale(_pointer);

    ///<inheritdoc/>
    protected override void Delete(IntPtr pointer) => manifold_delete_rect(pointer);
}
