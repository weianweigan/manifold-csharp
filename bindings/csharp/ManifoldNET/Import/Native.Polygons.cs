namespace ManifoldNET.Import;

internal static unsafe partial class Native
{
    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr manifold_polygons(IntPtr mem, ref IntPtr ps, ulong length);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern ulong manifold_polygons_length(IntPtr p);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern ulong manifold_polygons_simple_length(IntPtr p, int idx);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr manifold_polygons_get_simple(IntPtr mem, IntPtr ps, int idx);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern Vector2 manifold_polygons_get_point(IntPtr ps, int simple_idx, int idx);
}
