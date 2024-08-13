namespace ManifoldNETTests;

public class CrossSectionTests
{
    [Fact]
    public void Create_empty_instance()
    {
        CrossSection manifoldCrossSection = new CrossSection();

        Assert.NotNull(manifoldCrossSection);
    }

    [Fact]
    public void Create_by_rect_polygon()
    {
        SimplePolygon simplePolygon = CreateRectPolygon();

        CrossSection manifoldCrossSection = new CrossSection(simplePolygon, FillRule.Negative);

        Assert.NotNull(manifoldCrossSection);
    }

    [Fact]
    public void Create_by_polygons()
    {
        SimplePolygon simplePolygon1 = CreateRectPolygon();
        SimplePolygon simplePolygon2 = CreateRectPolygon2();
        Polygons polygons = new Polygons([simplePolygon1, simplePolygon2]);

        CrossSection manifoldCrossSection = new CrossSection(polygons, FillRule.Negative);

        Assert.NotNull(manifoldCrossSection);
    }

    [Fact]
    public void Create_then_get_bounds()
    {
        SimplePolygon simplePolygon = CreateRectPolygon();
        CrossSection cs = new(simplePolygon, FillRule.Positive);
        Assert.NotNull(cs);

        Rect rect = cs.Bounds();
        Assert.NotNull(rect);
    }

    [Fact]
    public void Create_then_get_polygons()
    {
        SimplePolygon simplePolygon = CreateRectPolygon();
        CrossSection cs = new(simplePolygon, FillRule.Positive);
        Assert.NotNull(cs);

        var polygons = cs.Polygons();

        Assert.NotNull(polygons);
        Assert.Equal(1, polygons.Count());
    }

    [Fact]
    public void Create_then_copy()
    {
        SimplePolygon simplePolygon = CreateRectPolygon();
        CrossSection cs = new(simplePolygon, FillRule.Positive);
        Assert.NotNull(cs);
        var cs2 = cs.Copy();
        Assert.NotNull(cs);
    }

    [Fact]
    public void Create_by_square()
    {
        CrossSection cs = CrossSection.Square(1, 2);
        Assert.NotNull(cs);
        Assert.Equal(2d, cs.Area);
        Assert.False(cs.IsEmpty);
        Assert.Equal(1, cs.ContourNumber);
        Assert.Equal(4, cs.VerticesNumber);
    }

    [Fact]
    public void Create_by_circle()
    {
        CrossSection cs = CrossSection.Circle(1, 3);
        Assert.NotNull(cs);
        Assert.False(cs.IsEmpty);
        Assert.Equal(1, cs.ContourNumber);
        Assert.Equal(3, cs.VerticesNumber);
    }

    [Fact]
    public void Intersect_two_polygons()
    {
        SimplePolygon simplePolygon = CreateRectPolygon();
        SimplePolygon simplePolygon2 = CreateRectPolygon2();
        CrossSection cs = new(simplePolygon, FillRule.Positive);
        CrossSection cs2 = new(simplePolygon2, FillRule.Positive);
        var cs3 = cs.Boolean(cs2, BoolOperationType.Intersect);
        Assert.NotNull(cs3);
        Assert.Equal(2, cs3.Area);
        Assert.False(cs3.IsEmpty);
        Assert.Equal(1, cs3.ContourNumber);
        Assert.Equal(4, cs3.VerticesNumber);
    }

    [Fact]
    public void Union_two_polygons()
    {
        SimplePolygon simplePolygon = CreateRectPolygon();
        SimplePolygon simplePolygon2 = CreateRectPolygon2();
        CrossSection cs = new(simplePolygon, FillRule.Positive);
        CrossSection cs2 = new(simplePolygon2, FillRule.Positive);
        var cs3 = cs.Union(cs2);
        Assert.NotNull(cs3);
        Assert.Equal(14, cs3.Area);
        Assert.False(cs3.IsEmpty);
        Assert.Equal(1, cs3.ContourNumber);
        Assert.Equal(7, cs3.VerticesNumber);
    }

    [Fact]
    public void Difference_two_polygons()
    {
        SimplePolygon simplePolygon = CreateRectPolygon();
        SimplePolygon simplePolygon2 = CreateRectPolygon2();
        CrossSection cs = new(simplePolygon, FillRule.Positive);
        CrossSection cs2 = new(simplePolygon2, FillRule.Positive);
        var cs3 = cs.Difference(cs2);
        Assert.NotNull(cs3);
    }

    [Fact]
    public void Hull()
    {
        SimplePolygon simplePolygon = CreateRectPolygon();
        CrossSection cs = new(simplePolygon, FillRule.Positive);
        CrossSection cs3 = cs.Hull();
        Assert.NotNull(cs3);

        SimplePolygon rect = cs3.Polygons().GetSimplePolygon(0);
        Assert.NotNull(rect);
        Assert.Equal(simplePolygon[0], rect[0]);
        Assert.Equal(simplePolygon[1], rect[1]);
        Assert.Equal(simplePolygon[2], rect[2]);
        Assert.Equal(simplePolygon[3], rect[3]);
    }

    [Fact]
    public void Translate()
    {
        SimplePolygon simplePolygon = CreateRectPolygon();
        CrossSection cs = new(simplePolygon, FillRule.Positive);
        var cs3 = cs.Translate(1, 1);
        Assert.NotNull(cs3);
    }

    [Fact]
    public void Rotate()
    {
        SimplePolygon simplePolygon = CreateRectPolygon();
        CrossSection cs = new(simplePolygon, FillRule.Positive);
        var cs3 = cs.Rotate((float)Math.PI);
        Assert.NotNull(cs3);
        var polygons1 = cs.Polygons().GetSimplePolygon(0);
        var polygons2 = cs3.Polygons().GetSimplePolygon(0);
    }

    [Fact]
    public void Scale()
    {
        SimplePolygon simplePolygon = CreateRectPolygon();
        CrossSection cs = new(simplePolygon, FillRule.Positive);
        var cs3 = cs.Scale(1, 1);
        Assert.NotNull(cs3);
    }

    [Fact]
    public void Mirror()
    {
        SimplePolygon simplePolygon = CreateRectPolygon();
        CrossSection cs = new(simplePolygon, FillRule.Positive);
        var cs3 = cs.Mirror(1, 1);
        Assert.NotNull(cs3);
    }

    [Fact]
    public void Transform()
    {
        SimplePolygon simplePolygon = CreateRectPolygon();
        CrossSection cs = new(simplePolygon, FillRule.Positive);
        var cs3 = cs.Transform(1, 1, 1, 1, 1, 1);
        Assert.NotNull(cs3);
    }

    //[Fact]
    //public void WarpTest()
    //{
    //    SimplePolygon simplePolygon = CreateRectPolygon();
    //    CrossSection cs = new(simplePolygon, FillRule.Positive);
    //    var cs3 = cs.Warp((x, y) => new Vector2(x, y));
    //    Assert.NotNull(cs3);
    //}

    [Fact]
    public void Decompose()
    {
        SimplePolygon simplePolygon1 = CreateRectPolygon();
        SimplePolygon simplePolygon2 = CreateRectPolygon2();
        Polygons polygons = new Polygons([simplePolygon1, simplePolygon2]);
        CrossSection cs = new CrossSection(polygons, FillRule.EvenOdd);
        var vec = cs.Decompose();
        Assert.NotNull(vec);
        Assert.Equal(2d, vec.Length);
    }

    [Fact]
    public void Simplify()
    {
        SimplePolygon simplePolygon = CreateRectPolygon();
        CrossSection cs = new(simplePolygon, FillRule.Positive);
        var cs3 = cs.Simplify(1);
        Assert.NotNull(cs3);
    }

    [Fact]
    public void Offset()
    {
        SimplePolygon simplePolygon = CreateRectPolygon();
        CrossSection cs = new(simplePolygon, FillRule.Positive);
        var cs3 = cs.Offset(1, JoinType.Miter, 1, 1);
        Assert.NotNull(cs3);
    }

    internal static SimplePolygon CreateRectPolygon()
    {
        return new SimplePolygon([new(0, 0), new(2, 0), new(2, 2), new(0, 2),]);
    }

    internal static SimplePolygon CreateRectPolygon2()
    {
        return new SimplePolygon([new(1, 0), new(4, 0), new(4, 4), new(1, 4),]);
    }
}
