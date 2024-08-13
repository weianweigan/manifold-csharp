#if MESH_EXPORT
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ManifoldNETTests;

public class MeshIOTests
{
    [Theory]
    [InlineData("cube.glb")]
    [InlineData("cube.gltf")]
    [InlineData("方块.gltf")]
    public void Export_to_file(string fileName)
    {
        // Prepare.
        Manifold cube = Manifold.Cube(1, 1, 1);
        var options = new ExportOptions()
        {
            Material = new Material() { Color = new Vector4(0.2f, 0.2f, 0.2f, 1) }
        };

        // Assert.
        if (MeshIO.ContainsUnicodeCharacter(ref fileName))
        {
            Assert.Throws<ArgumentException>(() => cube.MeshGL.ExportMeshGL(fileName, options));
        }
        else
        {
            cube.MeshGL.ExportMeshGL(fileName, options);
            Assert.True(File.Exists(fileName));
        }
    }

    [Fact]
    public void Import()
    {
#pragma warning disable SYSLIB0012
        // Xunit returns temp dir if using Location.
        var workdir = Path.GetDirectoryName(
            new Uri(Assembly.GetExecutingAssembly().CodeBase!).LocalPath
        );
#pragma warning restore SYSLIB0012

        Assert.NotNull(workdir);

        // root dir
        while (!Directory.GetDirectories(workdir).Any(p => p.EndsWith(".git")))
        {
            workdir = Directory.GetParent(workdir)!.FullName;
            var subDirs = Directory.GetDirectories(workdir);
        }

        IEnumerable<string> models = Directory
            .GetFiles(Path.Combine(workdir, "samples", "models"))
            .Where(p => Path.GetExtension(p).Equals(".glb", StringComparison.OrdinalIgnoreCase));

        foreach (var model in models)
        {
            var meshGL = MeshIO.ImportMeshGL(model, true);
            Assert.NotNull(meshGL);
        }
    }
}
#endif
