namespace ManifoldNET.Import;

internal static unsafe partial class Native
{
    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr manifold_manifold_empty_vec(IntPtr mem);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr manifold_manifold_vec(IntPtr mem, ulong sz);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void manifold_manifold_vec_reserve(IntPtr ms, ulong sz);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern ulong manifold_manifold_vec_length(IntPtr ms);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr manifold_manifold_vec_get(IntPtr mem, IntPtr ms, ulong idx);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void manifold_manifold_vec_set(IntPtr ms, int idx, IntPtr m);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void manifold_manifold_vec_push_back(IntPtr ms, IntPtr m);
}
