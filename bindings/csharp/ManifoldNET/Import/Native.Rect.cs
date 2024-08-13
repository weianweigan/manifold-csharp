namespace ManifoldNET.Import;

internal static unsafe partial class Native
{
    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr manifold_rect(IntPtr mem, float x1, float y1, float x2, float y2);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern Vector2 manifold_rect_min(IntPtr r);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern Vector2 manifold_rect_max(IntPtr r);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern Vector2 manifold_rect_dimensions(IntPtr r);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern Vector2 manifold_rect_center(IntPtr r);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern float manifold_rect_scale(IntPtr r);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int manifold_rect_contains_pt(IntPtr r, float x, float y);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int manifold_rect_contains_rect(IntPtr a, IntPtr b);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void manifold_rect_include_pt(IntPtr r, float x, float y);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr manifold_rect_union(IntPtr mem, IntPtr a, IntPtr b);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr manifold_rect_transform(
        IntPtr mem,
        IntPtr r,
        float x1,
        float y1,
        float x2,
        float y2,
        float x3,
        float y3
    );

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr manifold_rect_translate(IntPtr mem, IntPtr r, float x, float y);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr manifold_rect_mul(IntPtr mem, IntPtr r, float x, float y);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int manifold_rect_does_overlap_rect(IntPtr a, IntPtr r);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int manifold_rect_is_empty(IntPtr r);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int manifold_rect_is_finite(IntPtr r);
}
