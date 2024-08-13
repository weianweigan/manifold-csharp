namespace ManifoldNETTests;

public class PolygonsTests
{
    [Fact]
    public void Create_polygons()
    {
        using var rect1 = SimplePolygonTests.CreateRectPolygon();
        using var rect2 = SimplePolygonTests.CreateRectPolygon();

        using var polygons = new Polygons([rect1, rect2]);

        Assert.NotNull(polygons);
        Assert.Equal(2, polygons.Count());
        Assert.Equal(4, polygons.VecCountOfSimplePolygon(0));
        Assert.Equal(4, polygons.VecCountOfSimplePolygon(1));

        SimplePolygon rect1a = polygons.GetSimplePolygon(0);
        SimplePolygon rect2a = polygons.GetSimplePolygon(1);
        AssertRect(rect1, rect1a);
        AssertRect(rect2, rect2a);
        Assert.Equal(new Vector2(1, 0), polygons.GetPoint(0, 1));
        Assert.Equal(new Vector2(1, 0), polygons.GetPoint(1, 1));
    }

    private static void AssertRect(SimplePolygon excepted, SimplePolygon actual)
    {
        Assert.Equal(excepted.Count(), actual.Count());

        var count = excepted.Count();
        for (int i = 0; i < (int)count; i++)
        {
            Assert.Equal(excepted[i], actual[i]);
        }

        Assert.False(object.ReferenceEquals(actual, excepted));
    }
}
