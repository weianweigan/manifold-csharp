using System.Diagnostics;
using System.Linq;
using static ManifoldNET.Import.Native;

namespace ManifoldNET;

/// <summary>
/// A complex polygon composed of multiple <see cref="SimplePolygon"/>s.
/// </summary>
public sealed class Polygons : ManifoldObject
{
    private readonly IntPtr[]? _psArray;

    /// <summary>
    /// Initializes a new instance of <see cref="Polygons"/>.
    /// </summary>
    /// <param name="simplePolygons">Array of <see cref="SimplePolygon"/>.</param>
    public Polygons(SimplePolygon[] simplePolygons)
        : base(IntPtr.Zero)
    {
        _psArray = simplePolygons.Select(p => p.GetPointer()).ToArray();
        _pointer = manifold_polygons(
            Marshal.AllocHGlobal((int)manifold_polygons_size()),
            ref _psArray[0],
            (ulong)simplePolygons.Length
        );
    }

    internal Polygons(IntPtr pointer)
        : base(pointer) { }

    /// <summary>
    /// <see cref="SimplePolygon"/> Count.
    /// </summary>
    public int Count() => (int)manifold_polygons_length(_pointer);

    /// <summary>
    /// <see cref="Vector2"/> count of <see cref="SimplePolygon"/>.
    /// </summary>
    public int VecCountOfSimplePolygon(int idxOfPolygons) =>
        (int)manifold_polygons_simple_length(_pointer, idxOfPolygons);

    /// <summary>
    /// Gets <see cref="SimplePolygon"/> by index.
    /// </summary>
    /// <param name="idx"></param>
    /// <returns></returns>
    public unsafe SimplePolygon GetSimplePolygon(int idx)
    {
        ulong size = manifold_simple_polygon_size();
        IntPtr mem = Marshal.AllocHGlobal((int)size);
        var ptr = manifold_polygons_get_simple(mem, _pointer, idx);

        Debug.Assert(mem == ptr);

        return new SimplePolygon(ptr);
    }

    /// <summary>
    /// Gets an <see cref="Vector2"/> in one of <see cref="SimplePolygon"/>.
    /// </summary>
    /// <param name="simplePolygonIdx"><see cref="SimplePolygon"/> index of this.</param>
    /// <param name="pointIdxOfSimplePolygon"><see cref="Vector2"/> index of <see cref="SimplePolygon"/>.</param>
    /// <returns><see cref="Vector2"/> needed.</returns>
    public Vector2 GetPoint(int simplePolygonIdx, int pointIdxOfSimplePolygon)
    {
        return manifold_polygons_get_point(_pointer, simplePolygonIdx, pointIdxOfSimplePolygon);
    }

    ///<inheritdoc/>
    protected override void Delete(IntPtr pointer) => manifold_delete_polygons(pointer);
}
