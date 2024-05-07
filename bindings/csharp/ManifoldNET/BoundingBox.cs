using ManifoldNET.Native;
using System.Runtime.InteropServices;

namespace ManifoldNET;

/// <summary>
/// BoundingBox that contains min and max value.
/// </summary>
public sealed class BoundingBox : ManifoldObject
{
    internal static unsafe class Native
    {
        [DllImport("manifoldc", CallingConvention = CallingConvention.Cdecl)]
        public static extern nint manifold_box(nint mem, float x1, float y1, float z1, float x2, float y2, float z2);

        [DllImport("manifoldc", CallingConvention = CallingConvention.Cdecl)]
        public static extern Vector3 manifold_box_min(nint b);

        [DllImport("manifoldc", CallingConvention = CallingConvention.Cdecl)]
        public static extern Vector3 manifold_box_max(nint b);

        [DllImport("manifoldc", CallingConvention = CallingConvention.Cdecl)]
        public static extern Vector3 manifold_box_dimensions(nint b);

        [DllImport("manifoldc", CallingConvention = CallingConvention.Cdecl)]
        public static extern Vector3 manifold_box_center(nint b);

        [DllImport("manifoldc", CallingConvention = CallingConvention.Cdecl)]
        public static extern float manifold_box_scale(nint b);

        [DllImport("manifoldc", CallingConvention = CallingConvention.Cdecl)]
        public static extern int manifold_box_contains_pt(nint b, float a, float y, float z);

        [DllImport("manifoldc", CallingConvention = CallingConvention.Cdecl)]
        public static extern int manifold_box_contains_box(nint a, nint b);

        [DllImport("manifoldc", CallingConvention = CallingConvention.Cdecl)]
        public static extern void manifold_box_include_pt(nint b, float x, float y, float z);

        [DllImport("manifoldc", CallingConvention = CallingConvention.Cdecl)]
        public static extern nint manifold_box_union(nint mem, nint a, nint b);

        [DllImport("manifoldc", CallingConvention = CallingConvention.Cdecl)]
        public static extern nint manifold_box_transform(nint mem, nint b,
            float x1, float y1, float z1,
            float x2, float y2, float z2,
            float x3, float y3, float z3,
            float x4, float y4, float z4);

        [DllImport("manifoldc", CallingConvention = CallingConvention.Cdecl)]
        public static extern nint manifold_box_translate(nint mem, nint b, float x, float y, float z);

        [DllImport("manifoldc", CallingConvention = CallingConvention.Cdecl)]
        public static extern nint manifold_box_mul(nint mem, nint b, float x, float y, float z);

        [DllImport("manifoldc", CallingConvention = CallingConvention.Cdecl)]
        public static extern int manifold_box_does_overlap_pt(nint b, float x, float y, float z);

        [DllImport("manifoldc", CallingConvention = CallingConvention.Cdecl)]
        public static extern int manifold_box_does_overlap_box(nint a, nint b);

        [DllImport("manifoldc", CallingConvention = CallingConvention.Cdecl)]
        public static extern int manifold_box_is_finite(nint b);
    }

    internal BoundingBox(nint pointer) : base(pointer) { }

    /// <summary>
    /// Min value of boundingbox.
    /// </summary>
    public Vector3 Min => Native.manifold_box_min(_pointer);

    /// <summary>
    /// Max value of boundingbox.
    /// </summary>
    public Vector3 Max => Native.manifold_box_max(_pointer);

    /// <summary>
    /// Size of boundingbox.
    /// </summary>
    public Vector3 Size => Native.manifold_box_dimensions(_pointer);

    /// <summary>
    /// Initializess a new instance of boundingbox.
    /// </summary>
    /// <param name="min">Min value.</param>
    /// <param name="max">Max value.</param>
    public unsafe BoundingBox(Vector3 min, Vector3 max) : base(
        Native.manifold_box(
            Marshal.AllocHGlobal((int)MemorySizeNative.manifold_box_size()),
            min.x, min.y, min.z,
            max.x, max.y, max.z)) { }
}
