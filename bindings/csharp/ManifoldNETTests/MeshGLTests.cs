using ManifoldNETTests.Samples;

namespace ManifoldNETTests;

public class MeshGLTests
{
    [Fact]
    public void Check_cube_meshGL()
    {
        var newManifold = Manifold.Create(Manifold.Cube(1f, 1f, 1f).MeshGL);
        Assert.NotNull(newManifold);
        Assert.True(newManifold.Status == ManifoldError.NoError);
    }

    [Fact]
    public void Generate_bricks()
    {
        var bricks = Bricks.Walls(10, 10, 10);
        var newManifold = Manifold.Create(bricks.MeshGL);
        Assert.NotNull(newManifold);
        Assert.True(newManifold.Status == ManifoldError.NoError);
    }

    [Fact]
    public void MeshGLTest()
    {
        var meshGL = new MeshGL(
            [
                0f,
                0f,
                0f,
                0f,
                0f,
                1f,
                0f,
                1f,
                0f,
                0f,
                1f,
                1f,
                1f,
                0f,
                0f,
                1f,
                0f,
                1f,
                1f,
                1f,
                0f,
                1f,
                1f,
                1f
            ],
            [
                1,
                0,
                4,
                2,
                4,
                0,
                1,
                3,
                0,
                3,
                1,
                5,
                3,
                2,
                0,
                3,
                7,
                2,
                5,
                4,
                6,
                5,
                1,
                4,
                6,
                4,
                2,
                7,
                6,
                2,
                7,
                3,
                5,
                7,
                5,
                6
            ]
        );

        Manifold manifold = Manifold.Create(meshGL);
        Assert.Equal(ManifoldError.NoError, manifold.Status);
        Assert.Equal(1, manifold.Properties.volume);
    }
}
