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
public enum FillRule
{
    /// <summary>
    /// Only odd numbered sub-regions are filled.
    /// </summary>
    EvenOdd,

    /// <summary>
    /// Only non-zero sub-regions are filled.
    /// </summary>
    NonZero,

    /// <summary>
    /// Only sub-regions with winding counts &gt; 0 are filled.
    /// </summary>
    Positive,

    /// <summary>
    /// Only sub-regions with winding counts &lt; 0 are filled.
    /// </summary>
    Negative
}
