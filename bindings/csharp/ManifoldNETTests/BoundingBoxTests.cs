namespace ManifoldNETTests;

public class BoundingBoxTests
{
    [Fact]
    public void ManifoldBoxTest()
    {
        var min = new Vector3(0, 0, 0);
        var max = new Vector3(1, 1, 1);

        var box = new BoundingBox(min, max);

        Assert.NotNull(box);
        Assert.Equal(min, box.Min);
        Assert.Equal(max, box.Max);
    }

    [Theory()]
    [InlineData(0f, 0f, 0f, 2.5f, 2.5f, 5f)]
    [InlineData(0f, 1.2f, 1.3f, 3.5f, 3.5f, 5f)]
    public void SizeTest(float x1, float y1, float z1, float x2, float y2, float z2)
    {
        var min = new Vector3(x1, y1, z1);
        var max = new Vector3(x2, y2, z2);

        var box = new BoundingBox(min, max);
        Vector3 size = box.Size;

        Assert.Equal(new Vector3(x2 - x1, y2 - y1, z2 - z1), size);
    }
}
