namespace ManifoldNET.Import;

internal static unsafe partial class Native
{
    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr manifold_simple_polygon(
        IntPtr mem,
        [In] Vector2[] ps,
        ulong length
    );

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern ulong manifold_simple_polygon_length(IntPtr p);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern Vector2 manifold_simple_polygon_get_point(IntPtr p, int idx);
}
