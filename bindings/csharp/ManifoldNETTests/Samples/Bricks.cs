using System.Collections.Generic;
using System.Linq;

namespace ManifoldNETTests.Samples;

public class Bricks
{
    public const float INCHES = 25.4f;

    public static float BrickDepth = (3f + 5f / 8f) * INCHES;
    public static float BrickHeight = (2f + 1f / 4f) * INCHES;
    public static float BrickLength = (7f + 5f / 8f) * INCHES;
    public static float MortarGap = (3f / 8f) * INCHES;

    public static Manifold Brack() => Manifold.Cube(BrickLength, BrickDepth, BrickHeight);

    public static Manifold HalfBrick() =>
        Manifold.Cube((BrickLength - MortarGap) / 2f, BrickDepth, BrickHeight);

    public static Manifold Row(int length)
    {
        var items = new List<Manifold>(length);

        for (int i = 0; i < length; i++)
        {
            items.Add(Brack().Translate((BrickLength + MortarGap) * i, 0, 0));
        }

        return items.Aggregate(Manifold.Union);
    }

    public static Manifold Wall(int length, int height, int alternate = 0)
    {
        var items = new List<Manifold>(length * height);

        for (int i = 0; i < height; i++)
        {
            items.Add(
                Row(length)
                    .Translate(
                        ((i + alternate) % 2) * (BrickDepth + MortarGap),
                        0,
                        (BrickHeight + MortarGap) * i
                    )
            );
        }

        return items.Aggregate(Manifold.Union);
    }

    public static Manifold Walls(int length, int width, int height)
    {
        return (
            (Manifold[])(

                [
                    Wall(length, width),
                    Wall(width, height, 1).Rotate(0f, 0f, 90f).Translate(BrickDepth, 0, 0),
                    Wall(length, height, 1).Translate(0f, (width) * (BrickLength + MortarGap), 0),
                    Wall(width, height)
                        .Rotate(0f, 0f, 90f)
                        .Translate((length + 0.5f) * (BrickLength + MortarGap) - MortarGap, 0f, 0f)
                ]
            )
        ).Aggregate(Manifold.Union);
    }

    public static Manifold Floor(int length, int width)
    {
        List<Manifold> results = [Walls(length, width, 1)];
        if (length > 1 && width > 1)
        {
            results.Add(
                Floor(length - 1, width - 1)
                    .Translate(BrickDepth + MortarGap, BrickDepth + MortarGap, 0)
            );
        }
        if (length == 1 && width > 1)
        {
            results.Add(Row(width - 1).Rotate(0f, 0f, 90f));
        }
        if (width == 1 && length > 1)
        {
            results.Add(
                Row(length - 1).Translate(2 * (BrickDepth + MortarGap), BrickDepth + MortarGap, 0)
            );
        }
        results.Add(HalfBrick().Translate(BrickDepth + MortarGap, BrickDepth + MortarGap, 0));

        return results.Aggregate(Manifold.Union);
    }

    [Theory]
    [InlineData(10, 10, 10)]
    public static void Generates(int width, int length, int height)
    {
        Manifold bricks = Walls(width, length, height);
        Assert.NotNull(bricks);
    }

#if MESH_EXPORT
    [Fact]
    public static void Save_to_file()
    {
        Manifold bricks = Walls(10, 10, 10);

        bricks.MeshGL.ExportMeshGL(
            "bricks.glb",
            new ExportOptions()
            {
                Material = new Material() { Color = new Vector4(0.2f, 0.2f, 0.2f, 0) }
            }
        );
    }
#endif
}
