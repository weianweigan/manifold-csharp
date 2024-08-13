using static ManifoldNET.Import.Native;

namespace ManifoldNET;

/// <summary>
/// A closed contour composed of a set of points on a plane.
/// </summary>
public sealed class SimplePolygon : ManifoldObject
{
    /// <summary>
    /// Initializes a simple polygon.
    /// </summary>
    /// <param name="manifoldVec2sArray">List of <see cref="Vector2"/></param>
    public unsafe SimplePolygon(Vector2[] manifoldVec2sArray)
        : base(
            manifold_simple_polygon(
                Marshal.AllocHGlobal((int)manifold_simple_polygon_size()),
                manifoldVec2sArray,
                (ulong)manifoldVec2sArray.Length
            )
        ) { }

    /// <summary>
    /// Initializes a new instance of <see cref="SimplePolygon"/>.
    /// </summary>
    internal SimplePolygon(IntPtr pointer)
        : base(pointer) { }

    /// <summary>
    /// Points Count of polygon.
    /// </summary>
    public ulong Count() => manifold_simple_polygon_length(_pointer);

    /// <summary>
    /// Gets <see cref="Vector2"/> from index.
    /// </summary>
    /// <param name="idx">Index.</param>
    /// <returns>Returns <see cref="Vector2"/></returns>
    public Vector2 this[int idx] => manifold_simple_polygon_get_point(_pointer, idx);

    ///<inheritdoc/>
    protected override void Delete(IntPtr pointer) => manifold_delete_simple_polygon(pointer);
}
