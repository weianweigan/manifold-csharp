namespace ManifoldNETTests;

public class RectTests
{
    [Fact]
    public void Create_rect()
    {
        var min = new Vector2(0, 0);
        var max = new Vector2(1, 1);
        var rect = new Rect(min, max);

        Assert.NotNull(rect);
        Assert.Equal(min, rect.Min);
        Assert.Equal(max, rect.Max);
    }

    [Theory()]
    [InlineData(0f, 0f, 0f, 0.5f)]
    [InlineData(0f, 0.2f, 0.1f, 0.5f)]
    public void Size(float x1, float y1, float x2, float y2)
    {
        var min = new Vector2(x1, y1);
        var max = new Vector2(x2, y2);
        var rect = new Rect(min, max);

        Vector2 size = rect.Size;
        Assert.Equal(new Vector2(x2 - x1, y2 - y1), size);
    }
}
