namespace ManifoldNET.Import;

internal partial class Native
{
    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern ulong manifold_cross_section_size();

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern ulong manifold_cross_section_vec_size();

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern ulong manifold_simple_polygon_size();

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern ulong manifold_polygons_size();

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern ulong manifold_manifold_size();

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern ulong manifold_manifold_vec_size();

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern ulong manifold_manifold_pair_size();

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern ulong manifold_meshgl_size();

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern ulong manifold_box_size();

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern ulong manifold_rect_size();
}
