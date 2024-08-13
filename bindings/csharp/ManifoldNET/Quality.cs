using static ManifoldNET.Import.Native;

namespace ManifoldNET;

/// <summary>
/// These static properties control how circular shapes are quantized by
/// default on construction. If circularSegments is specified, it takes
/// precedence. If it is zero, then instead the minimum is used of the segments
/// calculated based on edge length and angle, rounded up to the nearest
/// multiple of four. To get numbers not divisible by four, circularSegments
/// must be specified.
/// </summary>
public static class Quality
{
    /// <summary>
    /// Sets an angle constraint the default number of circular segments for the
    /// <see cref="CrossSection.Circle(float, int)"/>,
    /// <see cref="Manifold.Cylinder(float, float, float, int, bool)"/>,
    /// <see cref="Manifold.Sphere(float, int)"/>, and
    /// <see cref="Manifold.Revolve(Polygons, int)"/>
    /// constructors.The number of segments will be rounded up
    /// to the nearest factor of four.
    /// </summary>
    /// <param name="angle">
    /// he minimum angle in degrees between consecutive segments. The
    /// angle will increase if the segments hit the minimum-edge length.
    /// Default is 10 degrees.
    /// </param>
    /// <returns></returns>
    public static void SetMinCircularAngle(float angle)
    {
        manifold_set_min_circular_angle(angle);
    }

    /// <summary>
    /// Sets a length constraint the default number of circular segments for the
    /// <see cref="CrossSection.Circle(float, int)"/>,
    /// <see cref="Manifold.Cylinder(float, float, float, int, bool)"/>,
    /// <see cref="Manifold.Sphere(float, int)"/>, and
    /// <see cref="Manifold.Revolve(Polygons, int)"/>
    /// constructors.The number of segments will be rounded up
    /// to the nearest factor of four.
    /// </summary>
    /// <param name="length">
    /// The minimum length of segments. The length will
    /// increase if the segments hit the minimum angle.Default is 1.0.
    /// </param>
    public static void SetMinCircularEdgeLength(float length)
    {
        manifold_set_min_circular_edge_length(length);
    }

    /// <summary>
    /// Determine the result of the
    /// <see cref="SetMinCircularAngle(float)"/>,
    /// <see cref="SetMinCircularEdgeLength(float)"/> and
    /// <see cref="SetCurcularSegments(int)"/> defaults.
    /// </summary>
    /// <param name="radius">
    /// For a given radius of circle, determine how many default
    /// segments there will be.
    /// </param>
    /// <returns></returns>
    public static int GetCurcularSegments(float radius) => manifold_get_circular_segments(radius);

    /// <summary>
    ///  Sets the default number of circular segments for the
    /// <see cref="CrossSection.Circle(float, int)"/>,
    /// <see cref="Manifold.Cylinder(float, float, float, int, bool)"/>,
    /// <see cref="Manifold.Sphere(float, int)"/>, and
    /// <see cref="Manifold.Revolve(Polygons, int)"/>
    /// constructors.Overrides the edge length and angle
    /// constraints and sets the number of segments to exactly this value.
    /// </summary>
    /// <param name="number">
    /// Number of circular segments. Default is 0, meaning no
    /// constraint is applied.
    /// </param>
    public static void SetCurcularSegments(int number) => manifold_set_circular_segments(number);
}
