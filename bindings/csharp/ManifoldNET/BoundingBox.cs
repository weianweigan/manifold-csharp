using static ManifoldNET.Import.Native;

namespace ManifoldNET;

/// <summary>
/// BoundingBox that contains min and max value.
/// </summary>
public sealed class BoundingBox : ManifoldObject
{
    /// <summary>
    /// Min value of bounding box.
    /// </summary>
    public Vector3 Min => manifold_box_min(_pointer);

    /// <summary>
    /// Max value of bounding box.
    /// </summary>
    public Vector3 Max => manifold_box_max(_pointer);

    /// <summary>
    /// Initializes a new instance of bounding box.
    /// </summary>
    /// <param name="min">Min value.</param>
    /// <param name="max">Max value.</param>
    public BoundingBox(Vector3 min, Vector3 max)
        : base(Marshal.AllocHGlobal((int)manifold_box_size()))
    {
        manifold_box(_pointer, min.x, min.y, min.z, max.x, max.y, max.z);
    }

    internal BoundingBox(IntPtr ptr)
        : base(ptr) { }

    /// <summary>
    /// Gets the dimensions of the bounding box represented by this instance.
    /// </summary>
    /// <value>
    /// A <see cref="Vector3"/> representing the size (dimensions) of the bounding box.
    /// </value>
    public Vector3 Size => manifold_box_dimensions(_pointer);

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"{nameof(BoundingBox)}:{{{Min}, {Max}}}";
    }

    ///<inheritdoc/>
    protected override void Delete(IntPtr pointer) => manifold_delete_box(pointer);
}
