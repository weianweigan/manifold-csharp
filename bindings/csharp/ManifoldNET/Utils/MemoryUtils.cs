using static ManifoldNET.Import.Native;

namespace ManifoldNET.Utils;

internal class MemoryUtils
{
    public static Manifold ManifoldOp(Func<IntPtr, IntPtr> operation)
    {
        return new Manifold(operation.Invoke(Marshal.AllocHGlobal((int)manifold_manifold_size())));
    }

    public static CrossSection CrossSectionOp(Func<IntPtr, IntPtr> operation)
    {
        return new CrossSection(
            operation.Invoke(Marshal.AllocHGlobal((int)manifold_cross_section_size()))
        );
    }

    public static (Manifold, Manifold) ManifoldOp2(
        Func<IntPtr, IntPtr, ManifoldManifoldPair> operation
    )
    {
        ulong size = manifold_manifold_size();

        ManifoldManifoldPair pair = operation.Invoke(
            Marshal.AllocHGlobal((int)size),
            Marshal.AllocHGlobal((int)size)
        );

        return (new Manifold(pair.First), new Manifold(pair.Second));
    }
}
