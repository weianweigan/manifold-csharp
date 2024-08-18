using System.Collections.Generic;
using System.Linq;

namespace ManifoldNETTests;

public class ManifoldTests
{
    [Theory]
    [InlineData(1f, 1f, 1f, 1f)]
    [InlineData(1f, 1f, 2.11f, 2.11f)]
    public void Create_cube(float x, float y, float z, float volumn)
    {
        Manifold manifold = Manifold.Cube(x, y, z);

        Assert.NotNull(manifold);
        Assert.NotNull(manifold.MeshGL);
        AssertCubeTrianglesCount(manifold);
        Assert.Equal(volumn, manifold.Properties.volume);
    }

    [Theory]
    [InlineData(1f, 1f, 1f)]
    public void Create_cylinder(float height, float raduisLow, float raduisHigh)
    {
        Manifold manifold = Manifold.Cylinder(
            height,
            raduisLow,
            raduisHigh,
            Quality.GetCurcularSegments(Math.Max(raduisLow, raduisHigh))
        );

        Assert.NotNull(manifold);
        Assert.NotNull(manifold.MeshGL);
        Assert.Equal(ManifoldError.NoError, manifold.Status);
    }

    [Theory]
    [InlineData(1f, 1)]
    public void Create_sphere(float raduis, int circularSegments)
    {
        Manifold manifold = Manifold.Sphere(raduis, circularSegments);

        Assert.NotNull(manifold);
        Assert.NotNull(manifold.MeshGL);
        Assert.Equal(ManifoldError.NoError, manifold.Status);
    }

    [Fact]
    public void Extrude_square()
    {
        CrossSection section = CrossSection.Square(1.0f, 1.0f, true);
        Manifold manifold = Manifold.Extrude(section.Polygons(), 1f, 0, 0);

        Assert.NotNull(manifold);
        Assert.NotNull(manifold.MeshGL);
        Assert.Equal(ManifoldError.NoError, manifold.Status);
        Assert.Equal(1f, manifold.Properties.volume);
        Assert.Equal(6f, manifold.Properties.surface_area);

        AssertCubeTrianglesCount(manifold);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(0.1f)]
    [InlineData(0.2f)]
    public void Extrude_circle(float degress)
    {
        CrossSection section = CrossSection.Circle(1.1f, 1);
        Manifold manifold = Manifold.Extrude(section.Polygons(), 3.3f, 0, twistDegrees: degress);

        Assert.NotNull(manifold);
        Assert.NotNull(manifold.MeshGL);
        Assert.Equal(ManifoldError.NoError, manifold.Status);
    }

    [Fact]
    public void Revolve()
    {
        CrossSection section = CrossSection.Circle(1.1f, 1);
        Manifold manifold = Manifold.Revolve(section.Polygons(), 1);

        Assert.NotNull(manifold);
        Assert.NotNull(manifold.MeshGL);
        Assert.Equal(ManifoldError.NoError, manifold.Status);
    }

    [Fact]
    public void Compose_and_decompose_test()
    {
        List<Manifold> manifolds = [Manifold.Cube(1, 1, 1), Manifold.Sphere(1, 1)];

        Manifold manifold = Manifold.Compose(manifolds);
        Assert.NotNull(manifold);

        IReadOnlyList<Manifold> decomposeItems = manifold.Decompose();
        Assert.NotNull(decomposeItems);
        Assert.Equal(2, decomposeItems.Count);
    }

    [Fact]
    public void Bool_with_cube_and_sphere()
    {
        Manifold cube = Manifold.Cube(1f, 1f, 1f);
        Manifold sphere = Manifold.Sphere(1.2f, Quality.GetCurcularSegments(1.2f));

        Manifold unionItem = Manifold.Union(cube, sphere);
        Assert.NotNull(unionItem);
    }

    private static void AssertCubeTrianglesCount(Manifold manifold)
    {
        // A cube has 12 vertices and 8 triangles.
        Assert.Equal(12, manifold.MeshGL.TriangleNumber);
        Assert.Equal(8, manifold.MeshGL.VerticesNumber);

        Assert.Equal(12, manifold.TriangleNumber);
        Assert.Equal(8, manifold.VerticesNumber);
    }
}
