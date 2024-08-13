namespace ManifoldNET.Import;

internal static unsafe partial class Native
{
    private const string DllName = "manifoldc";

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr manifold_box(
        IntPtr mem,
        float x1,
        float y1,
        float z1,
        float x2,
        float y2,
        float z2
    );

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern Vector3 manifold_box_min(IntPtr b);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern Vector3 manifold_box_max(IntPtr b);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern Vector3 manifold_box_dimensions(IntPtr b);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern Vector3 manifold_box_center(IntPtr b);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern float manifold_box_scale(IntPtr b);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int manifold_box_contains_pt(IntPtr b, float a, float y, float z);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int manifold_box_contains_box(IntPtr a, IntPtr b);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void manifold_box_include_pt(IntPtr b, float x, float y, float z);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr manifold_box_union(IntPtr mem, IntPtr a, IntPtr b);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr manifold_box_transform(
        IntPtr mem,
        IntPtr b,
        float x1,
        float y1,
        float z1,
        float x2,
        float y2,
        float z2,
        float x3,
        float y3,
        float z3,
        float x4,
        float y4,
        float z4
    );

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr manifold_box_translate(
        IntPtr mem,
        IntPtr b,
        float x,
        float y,
        float z
    );

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr manifold_box_mul(IntPtr mem, IntPtr b, float x, float y, float z);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int manifold_box_does_overlap_pt(IntPtr b, float x, float y, float z);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int manifold_box_does_overlap_box(IntPtr a, IntPtr b);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int manifold_box_is_finite(IntPtr b);
}
