using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManifoldNETTests;

public class ManifoldArrayTests
{
    [Fact]
    public void Create_by_size()
    {
        var manifoldVec = new ManifoldArray(5);

        Assert.NotNull(manifoldVec);
        Assert.Equal(5, manifoldVec.Length);

        manifoldVec[0] = SampleManifold();
        Assert.NotNull(manifoldVec[0]);
        Assert.NotNull(manifoldVec[0]);
    }

    [Fact]
    public void Items_constructor()
    {
        Manifold cubeManifold = SampleManifold();
        Manifold cubeManifold2 = SampleManifold();

        var manifoldVec = new ManifoldArray([cubeManifold, cubeManifold, cubeManifold2]);

        var ms = manifoldVec.ToList();

        Assert.NotNull(manifoldVec);
        Assert.Equal(3, manifoldVec.Length);
    }

    [Fact(Skip = "Not real reverse?")]
    public void ReverseTest()
    {
        Manifold cubeManifold = SampleManifold();
        Manifold cubeManifold2 = Manifold.Sphere(1, 1);

        var manifoldVec = new ManifoldArray([cubeManifold, cubeManifold, cubeManifold2]);

        Assert.Equal(manifoldVec[0].MeshGL.TriangleNumber, cubeManifold.MeshGL.TriangleNumber);
        Assert.NotEqual(manifoldVec[0].MeshGL.TriangleNumber, cubeManifold2.MeshGL.TriangleNumber);
        manifoldVec.Reverse();
        Assert.Equal(manifoldVec[0].MeshGL.TriangleNumber, cubeManifold2.MeshGL.TriangleNumber);
    }

    [Fact]
    public void Get_enumerator()
    {
        Manifold cubeManifold = SampleManifold();

        var manifoldVec = new ManifoldArray([cubeManifold, cubeManifold, cubeManifold]);

        foreach (var item in manifoldVec)
        {
            Assert.Equal(item.MeshGL.TriangleNumber, cubeManifold.MeshGL.TriangleNumber);
        }
    }

    private Manifold SampleManifold()
    {
        Manifold manifold = Manifold.Cube(1, 1, 1);
        return manifold;
    }
}
