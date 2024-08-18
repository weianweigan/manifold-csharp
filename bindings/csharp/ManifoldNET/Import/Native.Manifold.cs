namespace ManifoldNET.Import;

internal static unsafe partial class Native
{
    #region Others
    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr manifold_empty(IntPtr mem);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr manifold_copy(IntPtr mem, IntPtr m);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr manifold_tetrahedron(IntPtr mem);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate float SdfContextDelegate(float x, float y, float z, IntPtr ctx);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr manifold_level_set(
        IntPtr mem,
        MeshGL.SdfDelegate sdf,
        IntPtr bounds,
        float edge_length,
        float level,
        bool seq
    );

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr manifold_level_set_seq(
        IntPtr mem,
        MeshGL.SdfDelegate sdf,
        IntPtr bounds,
        float edge_length,
        float level
    );

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr manifold_level_set_context(
        IntPtr mem,
        SdfContextDelegate sdf,
        IntPtr bounds,
        float edge_length,
        float level,
        IntPtr ctx
    );

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr manifold_level_set_seq_context(
        IntPtr mem,
        SdfContextDelegate sdf,
        IntPtr bounds,
        float edge_length,
        float level,
        IntPtr ctx
    );

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr manifold_refine(IntPtr mem, IntPtr m, int refine);
    #endregion

    #region Base geometry
    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr manifold_cube(IntPtr mem, float x, float y, float z, int center);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr manifold_cylinder(
        IntPtr mem,
        float height,
        float radius_low,
        float radius_high,
        int circular_segments,
        int center
    );

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr manifold_sphere(IntPtr mem, float radius, int circular_segments);
    #endregion

    #region Feature geometry
    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr manifold_smooth(
        IntPtr mem,
        IntPtr mesh,
        ref int half_edges,
        float[] smoothness,
        int n_edges
    );

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr manifold_of_meshgl(IntPtr mem, IntPtr mesh);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr manifold_extrude(
        IntPtr mem,
        IntPtr cs,
        float height,
        int slices,
        float twist_degrees,
        float scale_x,
        float scale_y
    );

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr manifold_revolve(IntPtr mem, IntPtr cs, int circular_segments);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr manifold_compose(IntPtr mem, IntPtr ms);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr manifold_decompose(IntPtr mem, IntPtr m);
    #endregion

    #region Properties
    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr manifold_get_meshgl(IntPtr mem, IntPtr m);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr manifold_as_original(IntPtr mem, IntPtr m);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int manifold_original_id(IntPtr m);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int manifold_is_empty(IntPtr m);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern ManifoldError manifold_status(IntPtr m);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int manifold_num_vert(IntPtr m);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int manifold_num_edge(IntPtr m);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int manifold_num_tri(IntPtr m);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int manifold_genus(IntPtr m);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern ManifoldProperties manifold_get_properties(IntPtr m);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr manifold_bounding_box(IntPtr mem, IntPtr m);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern float manifold_precision(IntPtr m);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern uint manifold_reserve_ids(uint n);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void PropertiesDelegate(ref float new_prop, Vector3 position, IntPtr old_prop);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr manifold_set_properties(
        IntPtr mem,
        IntPtr m,
        int num_prop,
        Manifold.ManifoldVec3Delegate fun
    );

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr manifold_calculate_curvature(
        IntPtr mem,
        IntPtr m,
        int gaussian_idx,
        int mean_idx
    );
    #endregion

    #region Operations
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate ManifoldManifoldPair SplitDelegate(
        IntPtr mem_first,
        IntPtr mem_second,
        IntPtr a,
        IntPtr b
    );

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr manifold_boolean(
        IntPtr mem,
        IntPtr a,
        IntPtr b,
        BoolOperationType op
    );

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr manifold_batch_boolean(IntPtr mem, IntPtr ms, BoolOperationType op);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr manifold_union(IntPtr mem, IntPtr a, IntPtr b);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr manifold_difference(IntPtr mem, IntPtr a, IntPtr b);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr manifold_intersection(IntPtr mem, IntPtr a, IntPtr b);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern ManifoldManifoldPair manifold_split(
        IntPtr mem_first,
        IntPtr mem_second,
        IntPtr a,
        IntPtr b
    );

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern ManifoldManifoldPair manifold_split_by_plane(
        IntPtr mem_first,
        IntPtr mem_second,
        IntPtr m,
        float normal_x,
        float normal_y,
        float normal_z,
        float offset
    );

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr manifold_trim_by_plane(
        IntPtr mem,
        IntPtr m,
        float normal_x,
        float normal_y,
        float normal_z,
        float offset
    );

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr manifold_slice(IntPtr mem, IntPtr m, float height);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr manifold_project(IntPtr mem, IntPtr m);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr manifold_hull(IntPtr mem, IntPtr m);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr manifold_batch_hull(IntPtr mem, IntPtr ms);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr manifold_hull_pts(IntPtr mem, [In] Vector3[] ps, ulong length);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr manifold_translate(IntPtr mem, IntPtr m, float x, float y, float z);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr manifold_rotate(IntPtr mem, IntPtr m, float x, float y, float z);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr manifold_scale(IntPtr mem, IntPtr m, float x, float y, float z);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr manifold_transform(
        IntPtr mem,
        IntPtr m,
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
    public static extern IntPtr manifold_mirror(IntPtr mem, IntPtr m, float nx, float ny, float nz);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr manifold_warp(
        IntPtr mem,
        IntPtr m,
        Manifold.ManifoldVec3Delegate fun
    );
    #endregion
}
