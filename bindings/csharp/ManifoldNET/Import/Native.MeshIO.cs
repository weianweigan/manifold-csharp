namespace ManifoldNET.Import;

internal static unsafe partial class Native
{
#if MESH_EXPORT
    #region Material
    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr manifold_material(IntPtr mem);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void manifold_material_set_roughness(IntPtr mat, float roughness);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void manifold_material_set_metalness(IntPtr mat, float metalness);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void manifold_material_set_color(IntPtr mat, Vector4 color);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void manifold_material_set_vert_color(
        IntPtr mat,
        [In] Vector4[]? vert_color,
        ulong n_vert
    );
    #endregion

    #region Export options
    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr manifold_export_options(IntPtr mem);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void manifold_export_options_set_faceted(IntPtr options, int faceted);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void manifold_export_options_set_material(IntPtr options, IntPtr mat);
    #endregion

    #region MeshIO
    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    public static extern void manifold_export_meshgl(IntPtr filename, IntPtr mesh, IntPtr options);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
    public static extern IntPtr manifold_import_meshgl(
        IntPtr mem,
        IntPtr filename,
        int force_cleanup
    );
    #endregion

    #region Memory Size
    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern ulong manifold_material_size();

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern nuint mesh_export_options_size();
    #endregion

    #region Memory free and destruction
    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void manifold_delete_material(IntPtr m);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void manifold_delete_export_options(IntPtr m);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void manifold_destruct_material(IntPtr m);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void manifold_destruct_export_options(IntPtr m);
    #endregion

#endif
}
