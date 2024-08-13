namespace ManifoldNET;

/// <summary>
/// Filling rules defining which polygon sub-regions are considered to be
/// inside a given polygon, and which sub-regions will not(based on winding
/// numbers). See the[Clipper2
/// docs](http://www.angusj.com/clipper2/Docs/Units/Clipper/Types/FillRule.htm)
/// for a detailed explanation with illustrations.
/// </summary>
/// <remarks>
/// Adapted from Clipper2 docs:
/// <see href="http://www.angusj.com/clipper2/Docs/Units/Clipper/Types/FillRule.htm"/>
/// (Copyright © 2010-2023 Angus Johnson)
/// </remarks>
/// <summary>
/// Specifies the treatment of path/contour joins (corners) when offsetting
/// CrossSections.See the
/// <see href="http://www.angusj.com/clipper2/Docs/Units/Clipper/Types/JoinType.htm">Clipper2 doc</see>
/// for illustrations.
/// </summary>
public enum JoinType
{
    /// <summary>
    /// Squaring is applied uniformly at all joins where the internal
    /// join angle is less than 90 degrees. The squared edge will be at
    /// exactly the offset distance from the join vertex.
    /// </summary>
    Square,

    /// <summary>
    /// Rounding is applied to all joins that have convex external
    /// angles, and it maintains the exact offset distance from the join
    /// vertex.
    /// </summary>
    Round,

    /// <summary>
    /// There's a necessary limit to mitered joins (to avoid narrow
    /// angled joins producing excessively long and narrow
    /// <see href="http://www.angusj.com/clipper2/Docs/Units/Clipper.Offset/Classes/ClipperOffset/Properties/MiterLimit.htm">spikes</see>.
    /// So where mitered joins would exceed a given maximum miter distance
    /// (relative to the offset distance), these are 'squared' instead.
    /// </summary>
    Miter
}