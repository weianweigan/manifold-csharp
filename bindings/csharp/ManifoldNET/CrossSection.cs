using System.Diagnostics;
using static ManifoldNET.Import.Native;
using static ManifoldNET.Utils.MemoryUtils;

namespace ManifoldNET;

/// <summary>
/// Two-dimensional cross-sections guaranteed to be without self-intersections,
/// or overlaps between polygons(from construction onwards). This class makes
/// use of the <see href="http://www.angusj.com/clipper2/Docs/Overview.htm">Clipper2</see>
/// library for polygon clipping(boolean) and offsetting operations.
/// </summary>
public sealed class CrossSection : ManifoldObject
{
    /// <summary>
    /// Cross-section <see cref="CrossSection"/>
    /// </summary>
    public CrossSection()
        : base(
            manifold_cross_section_empty(Marshal.AllocHGlobal((int)manifold_cross_section_size()))
        ) { }

    internal CrossSection(IntPtr ptr)
        : base(ptr) { }

    /// <summary>
    /// The cross-section <see cref="CrossSection"/> of the Simple polygon<see cref="SimplePolygon"/>
    /// </summary>
    /// <param name="simplePolygon"></param>
    /// <param name="fillRule">Fill rule.</param>
    public CrossSection(SimplePolygon simplePolygon, FillRule fillRule)
        : base(
            manifold_cross_section_of_simple_polygon(
                Marshal.AllocHGlobal((int)manifold_cross_section_size()),
                simplePolygon.GetPointer(),
                fillRule
            )
        ) { }

    /// <summary>
    /// The cross-section <see cref="CrossSection"/> of the multiple polygon<see cref="SimplePolygon"/>
    /// </summary>
    /// <param name="manifoldPolygons"></param>
    /// <param name="fr"></param>
    public CrossSection(Polygons manifoldPolygons, FillRule fr)
        : base(
            manifold_cross_section_of_polygons(
                Marshal.AllocHGlobal((int)manifold_cross_section_size()),
                manifoldPolygons.GetPointer(),
                fr
            )
        ) { }

    /// <summary>
    /// Area.
    /// </summary>
    public double Area => manifold_cross_section_area(_pointer);

    /// <summary>
    /// Number of vertices.
    /// </summary>
    public int VerticesNumber => manifold_cross_section_num_vert(_pointer);

    /// <summary>
    /// Number of contours.
    /// </summary>
    public int ContourNumber => manifold_cross_section_num_contour(_pointer);

    /// <summary>
    /// Is empty of this <see cref="CrossSection"/>
    /// </summary>
    public bool IsEmpty => manifold_cross_section_is_empty(_pointer) == 1;

    /// <summary>
    /// Gets rectangle bounds of this <see cref="CrossSection"/>.
    /// </summary>
    /// <returns></returns>
    public Rect Bounds()
    {
        return new Rect(
            manifold_cross_section_bounds(
                Marshal.AllocHGlobal((int)manifold_cross_section_size()),
                _pointer
            )
        );
    }

    /// <summary>
    /// Gets <see cref="Polygons"/> of this <see cref="Polygons"/>.
    /// </summary>
    /// <returns><see cref="Polygons"/></returns>
    public Polygons Polygons()
    {
        return new Polygons(
            manifold_cross_section_to_polygons(
                Marshal.AllocHGlobal((int)manifold_cross_section_size()),
                _pointer
            )
        );
    }

    /// <summary>
    /// Create new <see cref="CrossSection"/> instance.
    /// </summary>
    /// <returns>Returns a new copy.</returns>
    public CrossSection Copy()
    {
        return CrossSectionOp(mem => manifold_cross_section_copy(mem, _pointer));
    }

    /// <summary>
    /// Create a new square <see cref="CrossSection"/>.
    /// </summary>
    /// <param name="x">With value.</param>
    /// <param name="y">Height value.</param>
    /// <param name="center">
    /// True makes a square center in (0,0),
    /// False makes start point is (0,0).</param>
    /// <returns>New <see cref="CrossSection"/>.</returns>
    public static CrossSection Square(float x, float y, bool center = true)
    {
        return CrossSectionOp(memory =>
            manifold_cross_section_square(memory, x, y, center ? 1 : 0)
        );
    }

    /// <summary>
    /// Create a circle <see cref="CrossSection"/>.
    /// </summary>
    /// <param name="radius">Radius.</param>
    /// <param name="circularSegments"><see cref="Quality.GetCurcularSegments(float)"/>.</param>
    /// <returns></returns>
    public static CrossSection Circle(float radius, int circularSegments)
    {
        return CrossSectionOp(memory =>
            manifold_cross_section_circle(memory, radius, circularSegments)
        );
    }

    /// <summary>
    /// Boolean with another <see cref="CrossSection"/>.
    /// </summary>
    /// <param name="b">Another <see cref="CrossSection"/>.</param>
    /// <param name="op">Op type.</param>
    /// <returns>New <see cref="CrossSection"/></returns>
    public CrossSection Boolean(CrossSection b, BoolOperationType op)
    {
        return CrossSectionOp(memory =>
            manifold_cross_section_boolean(memory, _pointer, b.GetPointer(), op)
        );
    }

    /// <summary>
    /// Union operation.
    /// </summary>
    /// <param name="b"></param>
    /// <returns></returns>
    public CrossSection Union(CrossSection b)
    {
        return CrossSectionOp(memory =>
            manifold_cross_section_union(memory, _pointer, b.GetPointer())
        );
    }

    /// <summary>
    /// Difference operation.
    /// </summary>
    /// <param name="b"></param>
    /// <returns></returns>
    public CrossSection Difference(CrossSection b)
    {
        return CrossSectionOp(memory =>
            manifold_cross_section_difference(memory, _pointer, b.GetPointer())
        );
    }

    /// <summary>
    /// Intersection operation.
    /// </summary>
    /// <param name="b"></param>
    /// <returns></returns>
    public CrossSection Intersection(CrossSection b)
    {
        return CrossSectionOp(memory =>
            manifold_cross_section_intersection(memory, _pointer, b.GetPointer())
        );
    }

    /// <summary>
    /// Convex hull.
    /// </summary>
    /// <returns></returns>
    public CrossSection Hull()
    {
        return CrossSectionOp(memory => manifold_cross_section_hull(memory, _pointer));
    }

    /// <summary>
    /// Translate <see cref="CrossSection"/>.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public CrossSection Translate(float x, float y)
    {
        return CrossSectionOp(memory => manifold_cross_section_translate(memory, _pointer, x, y));
    }

    /// <summary>
    /// Rotate.
    /// </summary>
    /// <param name="degrees"></param>
    /// <returns></returns>
    public CrossSection Rotate(float degrees)
    {
        return CrossSectionOp(memory => manifold_cross_section_rotate(memory, _pointer, degrees));
    }

    /// <summary>
    /// Scale.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public CrossSection Scale(float x, float y)
    {
        ulong size = manifold_cross_section_size();
        var memory = Marshal.AllocHGlobal((int)size);
        var ptr = manifold_cross_section_scale(memory, _pointer, x, y);
        Debug.Assert(memory == ptr);
        return new CrossSection(ptr);
    }

    /// <summary>
    /// Mirror.
    /// </summary>
    /// <param name="axisX"></param>
    /// <param name="axisY"></param>
    /// <returns></returns>
    public CrossSection Mirror(float axisX, float axisY)
    {
        var size = manifold_cross_section_size();
        var memory = Marshal.AllocHGlobal((int)size);
        var ptr = manifold_cross_section_mirror(memory, _pointer, axisX, axisY);
        Debug.Assert(memory == ptr);
        return new CrossSection(ptr);
    }

    /// <summary>
    /// Transform.
    /// </summary>
    /// <param name="x1"></param>
    /// <param name="y1"></param>
    /// <param name="x2"></param>
    /// <param name="y2"></param>
    /// <param name="x3"></param>
    /// <param name="y3"></param>
    /// <returns></returns>
    public CrossSection Transform(float x1, float y1, float x2, float y2, float x3, float y3)
    {
        var size = manifold_cross_section_size();
        var memory = Marshal.AllocHGlobal((int)size);
        var ptr = manifold_cross_section_transform(memory, _pointer, x1, y1, x2, y2, x3, y3);
        Debug.Assert(memory == ptr);
        return new CrossSection(ptr);
    }

    ///// <summary>
    ///// <see cref="CrossSection.Warp(ManifoldVec2Delegate)"/>
    ///// </summary>
    ///// <param name="x"></param>
    ///// <param name="y"></param>
    ///// <returns></returns>
    //[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    //public delegate Vector2 ManifoldVec2Delegate(float x, float y);

    ///// <summary>
    ///// Move the vertices of this CrossSection (creating a new one) according to
    ///// any arbitrary input function, followed by a union operation(with a
    ///// Positive fill rule) that ensures any introduced intersections are not
    /////  included in the result.
    ///// </summary>
    ///// <param name="manifoldVec2Delegate"></param>
    ///// <returns></returns>
    //public CrossSection Warp(ManifoldVec2Delegate manifoldVec2Delegate)
    //{
    //    var ptr = manifold_cross_section_warp(
    //        Marshal.AllocHGlobal((int)manifold_cross_section_size()),
    //        _pointer,
    //        manifoldVec2Delegate,
    //        IntPtr.Zero
    //    );
    //    Debug.Assert(Marshal.AllocHGlobal((int)manifold_cross_section_size()) == ptr);
    //    return new CrossSection(ptr);
    //}

    /// <summary>
    /// Decompose.
    /// </summary>
    /// <returns></returns>
    public CrossSectionArray Decompose()
    {
        return CrossSectionArray.CreatCrossSectionVecOfIntPtr(
            manifold_cross_section_decompose(
                Marshal.AllocHGlobal((int)manifold_cross_section_size()),
                _pointer
            )
        );
    }

    /// <summary>
    /// Simplify.
    /// </summary>
    /// <param name="epsilon"></param>
    /// <returns></returns>
    public CrossSection Simplify(double epsilon)
    {
        return CrossSectionOp(memory => manifold_cross_section_simplify(memory, _pointer, epsilon));
    }

    /// <summary>
    /// Offset.
    /// </summary>
    /// <param name="delta"></param>
    /// <param name="jt"></param>
    /// <param name="miterLimit"></param>
    /// <param name="circularSegments"></param>
    /// <returns></returns>
    public CrossSection Offset(double delta, JoinType jt, double miterLimit, int circularSegments)
    {
        return CrossSectionOp(memory =>
            manifold_cross_section_offset(memory, _pointer, delta, jt, miterLimit, circularSegments)
        );
    }

    ///<inheritdoc/>
    protected override void Delete(IntPtr pointer) => manifold_delete_cross_section(pointer);
}
