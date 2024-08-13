namespace ManifoldNETTests;

public class CrossSectionVecTests
{
    [Fact]
    public void Create_empty_sections()
    {
        CrossSectionArray vec = new CrossSectionArray(2);
        Assert.NotNull(vec);
    }

    [Fact]
    public void Create_then_get_section()
    {
        using SimplePolygon simplePolygon = CreateRectPolygon();
        CrossSection cs = new(simplePolygon, FillRule.Positive);
        CrossSectionArray vec = new CrossSectionArray(2);
        vec[0] = cs;
        Assert.NotNull(vec);
        var cs2 = vec[0];
        Assert.NotNull(cs2);
    }

    [Fact]
    public void Update_section()
    {
        using SimplePolygon simplePolygon = CreateRectPolygon();
        CrossSection cs = new(simplePolygon, FillRule.Positive);
        CrossSectionArray vec = new CrossSectionArray(2);
        vec[0] = cs;
        Assert.NotNull(vec);
    }

    [Fact]
    public void Reverse_section()
    {
        using SimplePolygon simplePolygon = CreateRectPolygon();
        using SimplePolygon simplePolygon2 = CreateRectPolygon();

        CrossSection cs = new(simplePolygon, FillRule.Positive);
        CrossSection cs2 = new(simplePolygon, FillRule.Positive);

        CrossSectionArray vec = new CrossSectionArray(2);
        vec[0] = cs;
        vec[1] = cs2;

        vec.Reserve(2);
        Assert.NotNull(vec);
        var list = new[] { vec[0], vec[1] };
    }

    [Fact]
    public void Push_back_section()
    {
        using SimplePolygon simplePolygon = CreateRectPolygon();
        CrossSection cs = new(simplePolygon, FillRule.Positive);
        CrossSectionArray vec = new CrossSectionArray(2);
        vec.PushBack(cs);
        Assert.NotNull(vec);
        Assert.Equal(3, vec.Length);
    }

    [Fact]
    public void Batch_boolean()
    {
        CrossSectionArray vec = new CrossSectionArray(2);
        vec.BatchBoolean(BoolOperationType.Subtract);
        Assert.NotNull(vec);
    }

    [Fact]
    public void Batch_hull()
    {
        CrossSectionArray vec = new CrossSectionArray(2);
        vec.BatchHull();
        Assert.NotNull(vec);
    }

    internal static SimplePolygon CreateRectPolygon()
    {
        return new SimplePolygon([new(0, 0), new(2, 0), new(2, 2), new(0, 2),]);
    }
}
