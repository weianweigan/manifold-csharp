namespace ManifoldNET.Import;

internal static unsafe partial class Native
{
    #region Static Quality Globals
    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void manifold_set_min_circular_angle(float degrees);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void manifold_set_min_circular_edge_length(float length);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void manifold_set_circular_segments(int number);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int manifold_get_circular_segments(float radius);
    #endregion
}
