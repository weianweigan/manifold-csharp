using static ManifoldNET.Import.Native;
using static ManifoldNET.Utils.MemoryUtils;

namespace ManifoldNET;

/// <summary>
/// A collection of cross-sections <see cref="CrossSectionArray"/>
/// </summary>
public sealed class CrossSectionArray : ManifoldObject
{
    /// <summary>
    /// Empty <see cref="CrossSectionArray"/>
    /// </summary>
    internal CrossSectionArray()
        : base(
            manifold_cross_section_empty_vec(
                Marshal.AllocHGlobal((int)manifold_cross_section_vec_size())
            )
        ) { }

    /// <summary>
    /// Constructs <see cref="CrossSectionArray"/>
    /// </summary>
    /// <param name="sz"></param>
    public CrossSectionArray(int sz)
        : base(
            manifold_cross_section_vec(
                Marshal.AllocHGlobal((int)manifold_cross_section_vec_size()),
                (ulong)sz
            )
        ) { }

    private CrossSectionArray(IntPtr pointer)
        : base(pointer) { }

    /// <summary>
    /// Length of this array.
    /// </summary>
    public int Length => (int)manifold_cross_section_vec_length(_pointer);

    internal static CrossSectionArray CreatCrossSectionVecOfIntPtr(IntPtr intPtr) => new(intPtr);

    /// <summary>
    /// The <see cref="CrossSection"/> from the set of cross-sections <see cref="CrossSectionArray"/> according to the index
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public CrossSection this[int index]
    {
        get => CrossSectionOp(memory => manifold_cross_section_vec_get(memory, _pointer, index));
        set => manifold_cross_section_vec_set(_pointer, index, value.GetPointer());
    }

    /// <summary>
    /// Reverse.
    /// </summary>
    /// <param name="sz"></param>
    public void Reserve(int sz) => manifold_cross_section_vec_reserve(_pointer, (ulong)sz);

    /// <summary>
    /// Push back.
    /// </summary>
    /// <remarks>
    /// Append an <see cref="CrossSection"/> to current <see cref="CrossSectionArray"/>.
    /// </remarks>
    /// <param name="crossSection"></param>
    public void PushBack(CrossSection crossSection) =>
        manifold_cross_section_vec_push_back(this.GetPointer(), crossSection.GetPointer());

    /// <summary>
    /// Batch boolean.
    /// </summary>
    /// <param name="op"></param>
    /// <returns></returns>
    public CrossSection BatchBoolean(BoolOperationType op)
    {
        return CrossSectionOp(memory => manifold_cross_section_batch_boolean(memory, _pointer, op));
    }

    /// <summary>
    /// Batch hull.
    /// </summary>
    /// <returns></returns>
    public CrossSection BatchHull()
    {
        return CrossSectionOp(memory => manifold_cross_section_batch_hull(memory, _pointer));
    }

    /// <summary>
    /// Compose.
    /// </summary>
    /// <returns></returns>
    public CrossSection Compose()
    {
        return CrossSectionOp(memory => manifold_cross_section_compose(memory, _pointer));
    }

    ///<inheritdoc/>
    protected override void Delete(IntPtr pointer) => manifold_delete_cross_section_vec(pointer);
}
