using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ManifoldNETTests.Samples;

public class CubeWithDents
{
    [Theory]
    [InlineData(5, true)]
    public Manifold Run(int n = 5, bool overlap = true)
    {
        var a = Manifold.Cube(n, n, 0.5f).Translate(-0.5f, -0.5f, -0.5f);

        List<Manifold> list = new List<Manifold>(n * n);
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                list.Add(Manifold.Sphere(overlap ? 0.45f : 0.55f, 50).Translate(i, j, 0));
            }
        }

        return Manifold.Difference(a, list.Aggregate(Manifold.Union));
    }

#if MESH_EXPORT
    [Theory]
    [InlineData("cubewithdents.glb")]
    public void Export(string fileName)
    {
        var m = Run();
        m.MeshGL.ExportMeshGL(fileName);
        Assert.True(File.Exists(fileName));
    }
#endif
}
