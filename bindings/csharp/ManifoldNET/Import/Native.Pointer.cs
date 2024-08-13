namespace ManifoldNET.Import;

internal static unsafe partial class Native
{
    #region Pointer free
    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void manifold_delete_cross_section(IntPtr c);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void manifold_delete_cross_section_vec(IntPtr csv);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void manifold_delete_simple_polygon(IntPtr p);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void manifold_delete_polygons(IntPtr p);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void manifold_delete_manifold(IntPtr m);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void manifold_delete_manifold_vec(IntPtr ms);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void manifold_delete_meshgl(IntPtr m);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void manifold_delete_box(IntPtr b);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void manifold_delete_rect(IntPtr r);
    #endregion

    #region Destruction
    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void manifold_destruct_cross_section(IntPtr c);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void manifold_destruct_cross_section_vec(IntPtr csv);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void manifold_destruct_simple_polygon(IntPtr p);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void manifold_destruct_polygons(IntPtr p);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void manifold_destruct_manifold(IntPtr m);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void manifold_destruct_manifold_vec(IntPtr ms);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void manifold_destruct_meshgl(IntPtr m);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void manifold_destruct_box(IntPtr b);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void manifold_destruct_rect(IntPtr r);
    #endregion
}
