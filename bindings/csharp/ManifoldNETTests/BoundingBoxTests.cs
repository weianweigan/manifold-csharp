namespace ManifoldNET.Tests;

public class BoundingBoxTests
{
    [Fact]
    public void ManifoldBoxTest()
    {
        // Arrange.
        var min = new Vector3(0, 0, 0);
        var max = new Vector3(1, 1, 1);

        // Act.
        var box = new BoundingBox(min, max);

        // Assert.
        Assert.NotNull(box);
        Assert.Equal(min, box.Min);
        Assert.Equal(max, box.Max);
    }

    [Theory()]
    [InlineData(0f, 0f, 0f, 0.5f, 0.5f, 5f)]
    [InlineData(0f, 0.2f, 0.1f, 0.5f, 0.5f, 5f)]
    public void SizeTest(
        float x1, float y1, float z1,
        float x2, float y2, float z2)
    {
        // Arrange.
        var min = new Vector3(x1, y1, z1);
        var max = new Vector3(x2, y2, z2);

        // Act.
        var box = new BoundingBox(min, max);

        // Assert.
        Vector3 size = box.Size;
        Assert.Equal(new Vector3(x2 - x1, y2 - y1, z2 - z1), size);
    }
}