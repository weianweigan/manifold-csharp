using System;
using System.Runtime.CompilerServices;
using static ManifoldNET.Import.Native;
using static ManifoldNET.Utils.MemoryUtils;

[assembly: InternalsVisibleTo("ManifoldNETTests")]

namespace ManifoldNET;

/// <summary>
/// Core class of this Manifold.NET.
/// </summary>
public sealed unsafe class Manifold : ManifoldObject
{
    internal Manifold(IntPtr pointer)
        : base(pointer) { }

    #region Simple geometry creation
    /// <summary>
    /// Generates a cube <see cref="Manifold"/>.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    /// <param name="center">If it makes (0, 0) centers.</param>
    /// <returns></returns>
    public static Manifold Cube(float x, float y, float z, bool center = false)
    {
        return ManifoldOp(mem => manifold_cube(mem, x, y, z, center ? 1 : 0));
    }

    /// <summary>
    /// Generates a cylinder <see cref="Manifold"/>.
    /// </summary>
    /// <param name="height"></param>
    /// <param name="radiusLow"></param>
    /// <param name="radiusHigh"></param>
    /// <param name="circularSegments"></param>
    /// <param name="center"></param>
    /// <returns></returns>
    public static Manifold Cylinder(
        float height,
        float radiusLow,
        float radiusHigh,
        int circularSegments,
        bool center = false
    )
    {
        ulong size = manifold_manifold_size();
        IntPtr mem = Marshal.AllocHGlobal((int)size);

        IntPtr ptr = manifold_cylinder(
            mem,
            height,
            radiusLow,
            radiusHigh,
            circularSegments,
            center ? 1 : 0
        );
        return new Manifold(ptr);
    }

    /// <summary>
    /// Generates a sphere <see cref="Manifold"/>.
    /// </summary>
    /// <param name="radius"></param>
    /// <param name="circularSegments"></param>
    /// <returns></returns>
    public static Manifold Sphere(float radius, int circularSegments)
    {
        return ManifoldOp(mem => manifold_sphere(mem, radius, circularSegments));
    }
    #endregion

    #region Feature geometry
    /// <summary>
    /// Smooth.
    /// </summary>
    /// <param name="mesh"></param>
    /// <param name="halfEdges"></param>
    /// <param name="smoothness"></param>
    /// <param name="nEdges"></param>
    /// <returns></returns>
    public static Manifold Smooth(MeshGL mesh, ref int halfEdges, float[] smoothness, int nEdges)
    {
        ulong size = manifold_manifold_size();
        IntPtr mem = Marshal.AllocHGlobal((int)size);
        var ptr = manifold_smooth(mem, mesh.GetPointer(), ref halfEdges, smoothness, nEdges);
        return new Manifold(ptr);
    }

    /// <summary>
    /// Convert a MeshGL into a Manifold, retaining its properties and merging only
    /// the positions according to the merge vectors.
    /// </summary>
    /// <remarks>
    /// All fields are read, making this structure suitable for a lossless round-trip
    /// of data from GetMeshGL.For multi-material input, use ReserveIDs to set a
    /// unique originalID for each material, and sort the materials into triangle
    /// runs.
    /// </remarks>
    /// <param name="mesh">meshGL The input MeshGL.</param>
    /// <returns>Returns an empty Manifold
    /// and set an Error Status if the result is not an oriented 2-manifold. Will
    /// collapse degenerate triangles and unnecessary vertices.</returns>
    public static Manifold Create(MeshGL mesh)
    {
        return ManifoldOp(mem => manifold_of_meshgl(mem, mesh.GetPointer()));
    }

    /// <summary>
    /// Constructs a manifold from a set of polygons by extruding them along the
    /// Z-axis.
    /// Note that high twistDegrees with small nDivisions may cause
    /// self-intersection.This is not checked here, and it is up to the user to
    /// choose the correct parameters.
    /// </summary>
    /// <param name="crossSection">A set of non-overlapping polygons to extrude.</param>
    /// <param name="height">Z-extent of extrusion.</param>
    /// <param name="slices">Number of extra copies of the crossSection to insert into
    /// the shape vertically; especially useful in combination with twistDegrees to
    /// avoid interpolation artifacts.Default is none.</param>
    /// <param name="scaleTop">Amount to scale the top (independently in X and Y). If the
    /// scale is {0, 0}, a pure cone is formed with only a single vertex at the top.
    /// Note that scale is applied after a twist.
    /// Default
    ///    {1, 1}.</param>
    /// <param name="twistDegrees">Amount to twist the top crossSection relative to the
    /// bottom, interpolated linearly for the divisions in between.</param>
    /// <returns></returns>
    public static Manifold Extrude(
        Polygons crossSection,
        float height,
        int slices,
        float twistDegrees,
        Vector2 scaleTop
    )
    {
        return ManifoldOp(mem =>
            manifold_extrude(mem, crossSection.GetPointer(), height, slices, twistDegrees, scaleTop)
        );
    }

    /// <summary>
    /// Revolve.
    /// </summary>
    /// <param name="crossSection"></param>
    /// <param name="circularSegments"></param>
    /// <returns></returns>
    public static Manifold Revolve(Polygons crossSection, int circularSegments)
    {
        return ManifoldOp(mem =>
            manifold_revolve(mem, crossSection.GetPointer(), circularSegments)
        );
    }

    /// <summary>
    /// Compose.
    /// </summary>
    /// <param name="manifolds"></param>
    /// <returns></returns>
    public static Manifold Compose(ManifoldArray manifolds)
    {
        return ManifoldOp(mem => manifold_compose(mem, manifolds.GetPointer()));
    }

    /// <summary>
    /// Decompose.
    /// </summary>
    /// <returns></returns>
    public ManifoldArray Decompose()
    {
        return new ManifoldArray(
            manifold_decompose(
                Marshal.AllocHGlobal((int)manifold_manifold_vec_size()),
                this.GetPointer()
            )
        );
    }
    #endregion

    #region Properties
    /// <summary>
    /// The most complete output of this library, returning a MeshGL that is designed
    /// to easily push into a renderer, including all interleaved vertex properties
    /// that may have been input.It also includes relations to all the input meshes
    /// that form a part of this result and the transforms applied to each.
    /// </summary>
    public MeshGL MeshGL =>
        new(manifold_get_meshgl(Marshal.AllocHGlobal((int)manifold_meshgl_size()), _pointer));

    /// <summary>
    /// This function condenses all coplanar faces in the relation, and
    /// collapses those edges.In the process the relation to ancestor meshes is lost
    /// and this new Manifold is marked an original.Properties are preserved, so if
    /// they do not match across an edge, that edge will be kept.
    /// </summary>
    public Manifold AsOriginal() => ManifoldOp(mem => manifold_as_original(mem, _pointer));

    /// <summary>
    /// If this mesh is an original, this returns its meshID that can be referenced
    /// by product manifolds' MeshRelation. If this manifold is a product, this
    /// returns -1.
    /// </summary>
    public int OriginalID => manifold_original_id(_pointer);

    /// <summary>
    /// Returns the reason for an input Mesh producing an empty Manifold. This Status
    /// only applies to Manifolds newly-created from an input Mesh - once they are
    /// combined into a new Manifold via operations, the status reverts to NoError,
    /// simply processing the problem mesh as empty.Likewise, empty meshes may still
    /// show NoError, for instance, if they are small enough relative to their
    /// precision to be collapsed to nothing.
    /// </summary>
    public ManifoldError Status => manifold_status(_pointer);

    /// <summary>
    /// Does the Manifold have any triangles?
    /// </summary>
    public bool IsEmpty => manifold_is_empty(_pointer) == 1;

    /// <summary>
    /// The number of vertices in the Manifold.
    /// </summary>
    public int VerticesNumber => manifold_num_vert(_pointer);

    /// <summary>
    /// The number of edges in the Manifold.
    /// </summary>
    public int EdgeNumber => manifold_num_edge(_pointer);

    /// <summary>
    /// The number of triangles in the Manifold.
    /// </summary>
    public int TriangleNumber => manifold_num_tri(_pointer);

    /// <summary>
    /// The genus is a topological property of the manifold, representing the number
    /// of "handles". A sphere is 0, torus 1, etc. It is only meaningful for a single
    /// mesh, so it is best to call Decompose() first.
    /// </summary>
    public int Genus => manifold_genus(_pointer);

    /// <summary>
    /// Returns the surface area and volume of the manifold.
    /// </summary>
    public ManifoldProperties Properties => manifold_get_properties(_pointer);

    /// <summary>
    /// Returns the axis-aligned bounding box of all the Manifold's vertices.
    /// </summary>
    public BoundingBox BoundingBox =>
        new(manifold_bounding_box(Marshal.AllocHGlobal((int)manifold_box_size()), _pointer));

    /// <summary>
    /// Returns the precision of this Manifold's vertices, which tracks the
    /// approximate rounding error over all the transforms and operations that have
    /// led to this state.Any triangles that are collinear within this precision are
    /// considered degenerate and removed. This is the value of epsilon; defining
    /// <see href="https://github.com/elalish/manifold/wiki/Manifold-Library#definition-of-%CE%B5-valid">epsilon;-valid</see>
    /// </summary>
    public float Precision => manifold_precision(_pointer);

    /// <summary>
    /// Returns the first of n sequential new unique mesh IDs for marking sets of
    /// triangles that can be looked up after further operations.Assign to
    /// MeshGL.runOriginalID vector.
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public uint ReserveIDs(int n) => manifold_reserve_ids((uint)n);

    // TODO: Error delegate now, need to fix next.
    /// <summary>
    /// Create a new copy of this manifold with updated vertex properties by
    /// supplying a function that takes the existing position and properties as
    /// input.You may specify any number of output properties, allowing creation and
    /// removal of channels.Note: undefined behavior will result if you read past
    /// the number of input properties or write past the number of output properties.
    /// </summary>
    internal Manifold SetProperties(int propertiesNumber, ManifoldVec3Delegate manifoldVec3Delegate)
    {
        return ManifoldOp(mem =>
            manifold_set_properties(mem, _pointer, propertiesNumber, manifoldVec3Delegate)
        );
    }

    /// <summary>
    /// Curvature is the inverse of the radius of curvature, and signed such that
    /// positive is convex, and negative is concave. There are two orthogonal
    /// principal curvatures at any point on a manifold, with one maximum and the
    /// other minimum.Gaussian curvature is their product, while mean
    /// curvature is their sum. This approximates them for every vertex and assigns
    /// them as vertex properties on the given channels.
    /// </summary>
    /// <param name="gaussianIdx">
    /// The property channel index in which to store the Gaussian
    /// curvature.An index &lt; 0 will be ignored (stores nothing). The property set
    /// will be automatically expanded to include the channel index specified.
    /// </param>
    /// <param name="meanIdx">
    ///  meanIdx The property channel index in which to store the mean
    /// curvature.An index &lt; 0 will be ignored (stores nothing). The property set
    /// will be automatically expanded to include the channel index specified.
    /// </param>
    public Manifold CalculateCurvature(int gaussianIdx, int meanIdx)
    {
        return ManifoldOp(mem => manifold_calculate_curvature(mem, _pointer, gaussianIdx, meanIdx));
    }
    #endregion

    #region Operations
    /// <summary>
    /// The central operation of this library: the Boolean combines two manifolds
    /// into another by calculating their intersections and removing the unused
    /// portions.
    /// <see href="https://github.com/elalish/manifold/wiki/Manifold-Library#definition-of-%CE%B5-valid">epsilon</see>
    /// inputs will produce epsilon;-valid output. epsilon;-invalid input may fail
    /// triangulation.
    ///
    /// These operations are optimized to produce nearly instant results if either
    /// input is empty or their bounding boxes do not overlap.
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="boolOperationType">The type of operation to perform.</param>
    /// <returns></returns>
    public static Manifold BooleanOperation(
        Manifold a,
        Manifold b,
        BoolOperationType boolOperationType
    )
    {
        return ManifoldOp(mem => manifold_boolean(mem, a._pointer, b._pointer, boolOperationType));
    }

    /// <summary>
    /// Perform the given boolean operation on a list of Manifolds. In case of
    /// Subtract, all Manifolds in the tail are difference from the head.
    /// </summary>
    public static Manifold BatchBoolOperation(
        ManifoldArray manifolds,
        BoolOperationType boolOperationType
    )
    {
        return ManifoldOp(mem =>
            manifold_batch_boolean(mem, manifolds.GetPointer(), boolOperationType)
        );
    }

    /// <summary>
    /// Union.
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static Manifold Union(Manifold a, Manifold b)
    {
        return ManifoldOp(mem => manifold_union(mem, a._pointer, b._pointer));
    }

    /// <summary>
    /// Difference.
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static Manifold Difference(Manifold a, Manifold b)
    {
        return ManifoldOp(mem => manifold_difference(mem, a._pointer, b._pointer));
    }

    /// <summary>
    /// Intersection.
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static Manifold Intersection(Manifold a, Manifold b)
    {
        return ManifoldOp(mem => manifold_intersection(mem, a._pointer, b._pointer));
    }

    /// <summary>
    /// Split cuts this manifold in two using the cutter manifold. The first result
    /// is the intersection, second is the difference.This is more efficient than
    /// doing them separately.
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static (Manifold, Manifold) Split(Manifold a, Manifold b)
    {
        return ManifoldOp2((mem1, mem2) => manifold_split(mem1, mem2, a._pointer, b._pointer));
    }

    /// <summary>
    /// Convenient version of Split() for a half-space.
    /// </summary>
    /// <remarks>
    /// This vector is normal to the cutting plane and its length does
    /// not matter.The first result is in the direction of this vector, the second
    /// result is on the opposite side.
    /// </remarks>
    /// <param name="normalX"></param>
    /// <param name="normalY"></param>
    /// <param name="normalZ"></param>
    /// <param name="offset">The distance of the plane from the origin in the
    /// direction of the normal vector.</param>
    /// <returns></returns>
    public (Manifold, Manifold) SplitByPlane(
        float normalX,
        float normalY,
        float normalZ,
        float offset
    )
    {
        return ManifoldOp2(
            (mem1, mem2) =>
                manifold_split_by_plane(mem1, mem2, _pointer, normalX, normalY, normalZ, offset)
        );
    }

    /// <summary>
    /// Slice mesh as a 2D <see cref="CrossSection"/> from 3D in given height.
    /// </summary>
    /// <param name="height">Slice height.</param>
    /// <returns>A new <see cref="CrossSection"/></returns>
    public CrossSection Slice(float height)
    {
        return CrossSectionOp(mem => manifold_slice(mem, _pointer, height));
    }

    /// <summary>
    /// Project manifold as a <see cref="CrossSection"/>.
    /// </summary>
    /// <returns>A new <see cref="CrossSection"/>.</returns>
    public CrossSection Project()
    {
        return CrossSectionOp(mem => manifold_project(mem, _pointer));
    }

    /// <summary>
    /// Convex hull.
    /// </summary>
    /// <returns></returns>
    public Manifold Hull()
    {
        return ManifoldOp(mem => manifold_hull(mem, _pointer));
    }

    /// <summary>
    /// Create 3d hull by points.
    /// </summary>
    /// <param name="points">Points.</param>
    /// <returns><see cref="Manifold"/>.</returns>
    public Manifold HullPoints(Vector3[] points)
    {
        return ManifoldOp(mem => manifold_hull_pts(mem, points, (ulong)points.Length));
    }

    /// <summary>
    /// Move this Manifold in space. This operation can be chained. Transforms are
    /// combined and applied lazily.
    /// </summary>
    public Manifold Translate(float x, float y, float z)
    {
        return ManifoldOp(mem => manifold_translate(mem, _pointer, x, y, z));
    }

    /// <summary>
    /// Applies an Euler angle rotation to the manifold, first about the X axis, then
    /// Y, then Z, in degrees.We use degrees so that we can minimize rounding error,
    /// and eliminate it completely for any multiples of 90 degrees.Additionally,
    /// more efficient code paths are used to update the manifold when the transforms
    /// only rotate by multiples of 90 degrees.This operation can be chained.
    /// Transforms are combined and applied lazily.
    /// </summary>
    /// <param name="x">xDegrees First rotation, degrees about the X-axis.</param>
    /// <param name="y">yDegrees Second rotation, degrees about the Y-axis.</param>
    /// <param name="z">zDegrees Third rotation, degrees about the Z-axis.</param>
    /// <returns></returns>
    public Manifold Rotate(float x, float y, float z)
    {
        return ManifoldOp(mem => manifold_rotate(mem, _pointer, x, y, z));
    }

    /// <summary>
    /// Scale this Manifold in space. This operation can be chained. Transforms are
    /// combined and applied lazily.
    /// </summary>
    public Manifold Scale(float x, float y, float z)
    {
        return ManifoldOp(mem => manifold_scale(mem, _pointer, x, y, z));
    }

    /// <summary>
    /// Transform this Manifold in space. The first three columns form a 3x3 matrix
    /// transform and the last is a translation vector.This operation can be
    /// chained.Transforms are combined and applied lazily.
    /// </summary>
    public Manifold Transform(
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
    )
    {
        return ManifoldOp(mem =>
            manifold_transform(mem, _pointer, x1, y1, z1, x2, y2, z2, x3, y3, z3, x4, y4, z4)
        );
    }

    /// <summary>
    ///  Mirror this Manifold over the plane described by the unit form of the given
    /// normal vector.If the length of the normal is zero, an empty Manifold is
    /// returned.This operation can be chained.Transforms are combined and applied
    /// lazily.
    /// </summary>
    /// <param name="nx">X value of the normal vector of the plane to be mirrored over.</param>
    /// <param name="ny">Y value of the normal vector of the plane to be mirrored over</param>
    /// <param name="nz">Z value of the normal vector of the plane to be mirrored over</param>
    public Manifold Mirror(float nx, float ny, float nz)
    {
        return ManifoldOp(mem => manifold_rotate(mem, _pointer, nx, ny, nz));
    }

    /// <summary>
    /// <see cref="Wrap(ManifoldVec3Delegate)"/>
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    /// <returns></returns>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate Vector3 ManifoldVec3Delegate(float x, float y, float z);

    /// <summary>
    /// This function does not change the topology, but allows the vertices to be
    /// moved according to any arbitrary input function.It is easy to create a
    /// function that warps a geometrically valid object into one which overlaps, but
    /// that is not checked here, so it is up to the user to choose their function
    /// with discretion.
    /// </summary>
    /// <param name="manifoldVec3Delegate">A function that modifies a given vertex position.</param>
    public Manifold Wrap(ManifoldVec3Delegate manifoldVec3Delegate)
    {
        return ManifoldOp(mem => manifold_warp(mem, _pointer, manifoldVec3Delegate));
    }
    #endregion

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"{nameof(Manifold)}:{{Triangles: {MeshGL.TriangleNumber}, BoundingBox:{BoundingBox.Size}, Volume:{Properties.volume}, Genus:{Genus}}}";
    }

    ///<inheritdoc/>
    protected override void Delete(IntPtr pointer) => manifold_delete_manifold(pointer);
}
