namespace ManifoldNETTests;

public partial class ManifoldOperatorTests
{
    [Fact]
    public void Add()
    {
        var c1 = Manifold.Cube(1, 1, 1, true);
        var c2 = Manifold.Cube(1, 1, 1, false);

        var result = c1 + c2;
        Assert.NotNull(result);
        Assert.Equal(1.875f, result.Properties.volume);
        Assert.Equal(10.5f, result.Properties.surface_area);
    }

    [Fact]
    public void Subtract()
    {
        var c1 = Manifold.Cube(1, 1, 1, true);
        var c2 = Manifold.Cube(1, 1, 1, false);

        var result = c1 - c2;
        Assert.NotNull(result);
        Assert.Equal(0.875f, result.Properties.volume);
        Assert.Equal(6f, result.Properties.surface_area);
    }

    [Fact]
    public void Intersect()
    {
        var c1 = Manifold.Cube(1, 1, 1, true);
        var c2 = Manifold.Cube(1, 1, 1, false);

        var result = c1 & c2;
        Assert.NotNull(result);
        Assert.Equal(0.125f, result.Properties.volume);
        Assert.Equal(1.5f, result.Properties.surface_area);
    }
}
