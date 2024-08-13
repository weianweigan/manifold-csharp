using static ManifoldNET.Import.Native;

namespace ManifoldNET;

/// <summary>
/// An alternative to MeshData for output suitable for pushing into graphics
/// libraries directly.This may not be manifold since the verts are duplicated
/// along property boundaries that do not match.The additional merge vectors
/// store this missing information, allowing the manifold to be reconstructed.
/// </summary>
/// <remarks>
/// src\manifold\include\manifold.h
/// </remarks>
public struct MeshGLData
{
    /// <summary>
    /// Number of properties per vertex, always >= 3.
    /// </summary>
    public uint numProp;

    /// <summary>
    /// Flat, GL-style interleaved list of all vertex properties: propVal =
    /// vertProperties[vert * numProp + propIdx]. The first three properties are
    /// always the position x, y, z.
    /// </summary>
    public float[] vertProperties;

    /// <summary>
    /// The vertex indices of the three triangle corners in CCW (from the outside)
    /// order, for each triangle.
    /// </summary>
    public uint[] triVerts;

    /// <summary>
    /// Optional: The X-Y-Z-W weighted tangent vectors for smooth Refine(). If
    /// non-empty, must be exactly four times as long as MeshData.triVerts. Indexed
    /// as 4 * (3 * tri + i) + j, i &lt; 3, j &lt; 4, representing the tangent value
    /// Mesh.triVerts[tri][i] along the CCW edge. If empty, mesh is faceted.
    /// </summary>
    public float[]? halfedgeTangent;

    /// <summary>
    /// Initializes a new instance of <see cref="MeshGLData"/>.
    /// </summary>
    /// <param name="vertProperties">Vertices properties.</param>
    /// <param name="triVerts">Triangle vertices.</param>
    /// <param name="halfedgeTangent"></param>
    /// <param name="numProp"></param>
    public MeshGLData(
        float[] vertProperties,
        uint[] triVerts,
        uint numProp = 3,
        float[]? halfedgeTangent = null
    )
    {
        this.numProp = numProp;
        this.vertProperties = vertProperties;
        this.triVerts = triVerts;
        this.halfedgeTangent = halfedgeTangent;
    }
}

/// <summary>
/// MeshGL
/// </summary>
public sealed unsafe class MeshGL : ManifoldObject
{
    private readonly bool _cOwner;

    /// <summary>
    /// Delegate that returns a float value.
    /// </summary>
    /// <param name="x">X value.</param>
    /// <param name="y">Y value.</param>
    /// <param name="z">Z value.</param>
    /// <returns></returns>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate float SdfDelegate(float x, float y, float z);

    /// <summary>
    /// Initializes a new instance of <see cref="MeshGL"/>.
    /// </summary>
    /// <param name="meshGL">mesh data.</param>
    /// <exception cref="ArgumentException">Error properties length.</exception>
    public MeshGL(MeshGLData meshGL)
        : this(meshGL.vertProperties, meshGL.triVerts, meshGL.numProp, meshGL.halfedgeTangent) { }

    /// <summary>
    /// Initializes a new instance of <see cref="MeshGL"/>.
    /// </summary>
    /// <param name="vertProperties"></param>
    /// <param name="triVerts"></param>
    /// <param name="numProp"></param>
    /// <param name="halfedgeTangent"></param>
    /// <exception cref="ArgumentNullException"><paramref name="vertProperties"/> is null.</exception>
    /// <exception cref="ArgumentException">Array length is not right.</exception>
    public MeshGL(
        float[] vertProperties,
        uint[] triVerts,
        uint numProp = 3,
        float[]? halfedgeTangent = null
    )
        : base(IntPtr.Zero)
    {
        if (vertProperties == null)
        {
            throw new ArgumentNullException(nameof(vertProperties));
        }
        if (vertProperties.Length % numProp != 0 || vertProperties.Length == 0)
        {
            throw new ArgumentException(
                $"Length of {nameof(vertProperties)}({vertProperties.Length}) should be a multiple of {numProp}"
            );
        }

        if (halfedgeTangent == null)
        {
            _pointer = manifold_meshgl(
                Marshal.AllocHGlobal((int)manifold_meshgl_size()),
                vertProperties,
                (ulong)vertProperties.Length / numProp,
                numProp,
                triVerts,
                (ulong)triVerts.Length / 3
            );
        }
        else
        {
            _pointer = manifold_meshgl_w_tangents(
                Marshal.AllocHGlobal((int)manifold_meshgl_size()),
                vertProperties,
                (ulong)vertProperties.Length / numProp,
                numProp,
                triVerts,
                (ulong)triVerts.Length / 3,
                halfedgeTangent
            );
        }
    }

    internal MeshGL(IntPtr meshGLPtr, bool cOwner = false)
        : base(meshGLPtr) // Alloc by c, not Marshal ?
    {
        _pointer = meshGLPtr;
        _cOwner = cOwner;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="sdf"></param>
    /// <param name="boundingBox"></param>
    /// <param name="edgeLength"></param>
    /// <param name="level"></param>
    /// <param name="seq"></param>
    /// <returns></returns>
    public static MeshGL LevelSet(
        SdfDelegate sdf,
        BoundingBox boundingBox,
        float edgeLength,
        float level,
        bool seq
    )
    {
        return new MeshGL(
            manifold_level_set(
                Marshal.AllocHGlobal((int)manifold_meshgl_size()),
                sdf,
                boundingBox.GetPointer(),
                edgeLength,
                level,
                seq
            )
        );
    }

    #region Number or Length
    /// <summary>
    /// Length of properties.
    /// </summary>
    public int PropertiesNumber => manifold_meshgl_num_prop(_pointer);

    /// <summary>
    /// Number of vertices.
    /// </summary>
    public int VerticesNumber => manifold_meshgl_num_vert(_pointer);

    /// <summary>
    /// Number of triangles.
    /// </summary>
    public int TriangleNumber => manifold_meshgl_num_tri(_pointer);

    /// <summary>
    /// Length of vertices properties.
    /// </summary>
    public ulong VerticesPropertiesLength => manifold_meshgl_vert_properties_length(_pointer);

    /// <summary>
    /// Lengt of triangles.
    /// </summary>
    public ulong TriangleLength => manifold_meshgl_tri_length(_pointer);

    /// <summary>
    /// Merge length.
    /// </summary>
    public ulong MergeLength => manifold_meshgl_merge_length(_pointer);

    /// <summary>
    /// Run index length.
    /// </summary>
    public ulong RunIndexLength => manifold_meshgl_run_index_length(_pointer);

    /// <summary>
    /// Run original id length.
    /// </summary>
    public ulong RunOriginalIdLength => manifold_meshgl_run_original_id_length(_pointer);

    /// <summary>
    /// Run transform length.
    /// </summary>
    public ulong RunTransformLength => manifold_meshgl_run_transform_length(_pointer);

    /// <summary>
    /// Face id length.
    /// </summary>
    public ulong FaceIdLength => manifold_meshgl_face_id_length(_pointer);

    /// <summary>
    /// Tangent length.
    /// </summary>
    public ulong TangentLength => manifold_meshgl_tangent_length(_pointer);
    #endregion

    #region Data
    /// <summary>
    /// Vertices of properties.
    /// </summary>
    public float[]? VerticesProperties
    {
        get
        {
            var data = new float[VerticesPropertiesLength];
            IntPtr ptr = Marshal.AllocHGlobal((int)(sizeof(float) * VerticesPropertiesLength));
            manifold_meshgl_vert_properties(ptr, _pointer);
            Marshal.Copy(ptr, data, 0, data.Length);
            Marshal.FreeHGlobal(ptr);
            return data;
        }
    }

    /// <summary>
    /// Triangles vertices.
    /// </summary>
    public int[]? TriangleVertices
    {
        get
        {
            var data = new int[TriangleLength];
            IntPtr ptr = Marshal.AllocHGlobal((int)(sizeof(uint) * TriangleLength));
            manifold_meshgl_tri_verts(ptr, _pointer);
            Marshal.Copy(ptr, data, 0, data.Length);
            Marshal.FreeHGlobal(ptr);
            return data;
        }
    }
    #endregion

    ///<inheritdoc/>
    protected override void Delete(IntPtr pointer) => manifold_delete_meshgl(pointer);
}
