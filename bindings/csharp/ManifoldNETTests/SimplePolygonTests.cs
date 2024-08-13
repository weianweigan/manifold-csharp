using System.Collections.Generic;

namespace ManifoldNETTests;

public class SimplePolygonTests
{
    [Fact]
    public void Create_simple_polygons()
    {
        using SimplePolygon simplePolygon = CreateRectPolygon();

        Assert.NotNull(simplePolygon);
        Assert.Equal(4, (int)simplePolygon.Count());
        Assert.Equal(new Vector2(1, 0), simplePolygon[1]);
    }

    [Fact]
    public void Create_by_list()
    {
        using var simplePolygon = new SimplePolygon(
            new List<Vector2> { new(0, 0), new(1, 0), new(1, 1), new(0, 1), }.ToArray()
        );

        Assert.NotNull(simplePolygon);
        Assert.Equal(4, (int)simplePolygon.Count());
        Assert.Equal(new Vector2(1, 0), simplePolygon[1]);
    }

    internal static SimplePolygon CreateRectPolygon()
    {
        return new SimplePolygon([new(0, 0), new(1, 0), new(1, 1), new(0, 1),]);
    }
}
