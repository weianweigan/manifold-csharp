namespace ManifoldNET.Import;

internal unsafe partial class Native
{
    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr manifold_cross_section_empty(IntPtr mem);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr manifold_cross_section_copy(IntPtr mem, IntPtr cs);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr manifold_cross_section_of_simple_polygon(
        IntPtr mem,
        IntPtr p,
        FillRule fr
    );

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr manifold_cross_section_of_polygons(
        IntPtr mem,
        IntPtr p,
        FillRule fr
    );

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr manifold_cross_section_empty_vec(IntPtr mem);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr manifold_cross_section_vec(IntPtr mem, ulong sz);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void manifold_cross_section_vec_reserve(IntPtr csv, ulong sz);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern ulong manifold_cross_section_vec_length(IntPtr csv);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr manifold_cross_section_vec_get(IntPtr mem, IntPtr csv, int idx);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void manifold_cross_section_vec_set(IntPtr csv, int idx, IntPtr cs);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void manifold_cross_section_vec_push_back(IntPtr csv, IntPtr cs);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr manifold_cross_section_square(
        IntPtr mem,
        float x,
        float y,
        int center
    );

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr manifold_cross_section_circle(
        IntPtr mem,
        float radius,
        int circular_segments
    );

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr manifold_cross_section_boolean(
        IntPtr mem,
        IntPtr a,
        IntPtr b,
        BoolOperationType op
    );

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr manifold_cross_section_batch_boolean(
        IntPtr mem,
        IntPtr csv,
        BoolOperationType op
    );

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr manifold_cross_section_union(IntPtr mem, IntPtr a, IntPtr b);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr manifold_cross_section_difference(IntPtr mem, IntPtr a, IntPtr b);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr manifold_cross_section_intersection(IntPtr mem, IntPtr a, IntPtr b);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr manifold_cross_section_hull(IntPtr mem, IntPtr cs);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr manifold_cross_section_batch_hull(IntPtr mem, IntPtr css);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr manifold_cross_section_hull_simple_polygon(IntPtr mem, IntPtr ps);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr manifold_cross_section_hull_polygons(IntPtr mem, IntPtr ps);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr manifold_cross_section_translate(
        IntPtr mem,
        IntPtr cs,
        float x,
        float y
    );

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr manifold_cross_section_rotate(IntPtr mem, IntPtr cs, float deg);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr manifold_cross_section_scale(
        IntPtr mem,
        IntPtr cs,
        float x,
        float y
    );

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr manifold_cross_section_mirror(
        IntPtr mem,
        IntPtr cs,
        float ax_x,
        float ax_y
    );

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr manifold_cross_section_transform(
        IntPtr mem,
        IntPtr cs,
        float x1,
        float y1,
        float x2,
        float y2,
        float x3,
        float y3
    );

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr manifold_cross_section_simplify(
        IntPtr mem,
        IntPtr cs,
        double epsilon
    );

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr manifold_cross_section_offset(
        IntPtr mem,
        IntPtr cs,
        double delta,
        JoinType jt,
        double miter_limit,
        int circular_segments
    );

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern double manifold_cross_section_area(IntPtr cs);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int manifold_cross_section_num_vert(IntPtr cs);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int manifold_cross_section_num_contour(IntPtr cs);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int manifold_cross_section_is_empty(IntPtr cs);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr manifold_cross_section_bounds(IntPtr mem, IntPtr cs);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr manifold_cross_section_to_polygons(IntPtr mem, IntPtr cs);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr manifold_cross_section_compose(IntPtr mem, IntPtr csv);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr manifold_cross_section_decompose(IntPtr mem, IntPtr cs);
}
