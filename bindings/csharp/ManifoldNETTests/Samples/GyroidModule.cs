using System.IO;

namespace ManifoldNETTests.Samples;

public class GyroidModule
{
    public static float Gyroid(float x, float y, float z)
    {
        double xi = x - Math.PI / 4f;
        double yi = y - Math.PI / 4f;
        double zi = z - Math.PI / 4f;
        return (float)(
            Math.Cos(xi) * Math.Sin(yi) + Math.Cos(yi) * Math.Sin(zi) + Math.Cos(zi) * Math.Sin(xi)
        );
    }

    public static Manifold GyroidLevelSet(float level, float period, float size, int n) =>
        Manifold
            .Create(
                MeshGL.LevelSet(
                    Gyroid,
                    new BoundingBox(
                        new Vector3(-period, -period, -period),
                        new Vector3(period, period, period)
                    ),
                    period / n,
                    level,
                    true
                )
            )
            .Scale(size / period, size / period, size / period);

    public static Manifold RhombicDodecahedron(float size)
    {
        float v = size * (float)Math.Sqrt(2f);
        Manifold box = Manifold.Cube(v, v, v * 2f, true);
        var result = Manifold.Intersection(box.Rotate(90, 45, 0), box.Rotate(90, 35, 90));
        return Manifold.Intersection(box, result);
    }

    public static Manifold GenerateGyroidModule(float size = 20, int n = 15)
    {
        float period = (float)Math.PI * 2f;
        var result = Manifold.Difference(
            Manifold.Intersection(
                GyroidLevelSet(-0.4f, period, size, n),
                RhombicDodecahedron(size)
            ),
            GyroidLevelSet(0.4f, period, size, n)
        );
        return result.Rotate(-45f, 0f, 90f).Translate(0, 0, size / (float)Math.Sqrt(2f));
    }

    [Theory]
    [InlineData(20f, 15)]
    public static Manifold Generates(float size, int n) => GenerateGyroidModule(size, n);

#if MESH_EXPORT
    [Theory]
    [InlineData(20f, 15)]
    public static Manifold Save_to_file(float size, int n)
    {
        var m = GenerateGyroidModule(size, n);
        string fileName = $"{nameof(GyroidModule).ToLower()}.glb";
        m.MeshGL.ExportMeshGL(fileName);
        Assert.True(File.Exists(fileName));
        return m;
    }
#endif
}
