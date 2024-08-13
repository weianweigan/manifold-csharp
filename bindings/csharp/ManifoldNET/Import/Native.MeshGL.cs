namespace ManifoldNET.Import;

internal static unsafe partial class Native
{
    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr manifold_meshgl(
        IntPtr mem,
        float[] vert_props,
        ulong n_verts,
        ulong n_props,
        uint[] tri_verts,
        ulong n_tris
    );

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr manifold_meshgl_w_tangents(
        IntPtr mem,
        float[] vert_props,
        ulong n_verts,
        ulong n_props,
        uint[] tri_verts,
        ulong n_tris,
        float[] halfedge_tangent
    );

    #region Number or length
    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int manifold_meshgl_num_prop(IntPtr m);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int manifold_meshgl_num_vert(IntPtr m);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int manifold_meshgl_num_tri(IntPtr m);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern ulong manifold_meshgl_vert_properties_length(IntPtr m);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern ulong manifold_meshgl_tri_length(IntPtr m);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern ulong manifold_meshgl_merge_length(IntPtr m);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern ulong manifold_meshgl_run_index_length(IntPtr m);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern ulong manifold_meshgl_run_original_id_length(IntPtr m);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern ulong manifold_meshgl_run_transform_length(IntPtr m);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern ulong manifold_meshgl_face_id_length(IntPtr m);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern ulong manifold_meshgl_tangent_length(IntPtr m);
    #endregion

    #region Data
    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern float* manifold_meshgl_vert_properties(IntPtr mem, IntPtr m);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern uint* manifold_meshgl_tri_verts(IntPtr mem, IntPtr m);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern uint* manifold_meshgl_merge_from_vert(IntPtr mem, IntPtr m);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern uint* manifold_meshgl_merge_to_vert(IntPtr mem, IntPtr m);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern uint* manifold_meshgl_run_index(IntPtr mem, IntPtr m);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern uint* manifold_meshgl_run_original_id(IntPtr mem, IntPtr m);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern uint* manifold_meshgl_run_transform(IntPtr mem, IntPtr m);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern uint* manifold_meshgl_face_id(IntPtr mem, IntPtr m);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern float* manifold_meshgl_halfedge_tangent(IntPtr mem, IntPtr m);
    #endregion
}
