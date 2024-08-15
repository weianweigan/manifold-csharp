# ManifoldNET

![NuGet Downloads](https://img.shields.io/nuget/dt/ManifoldNET.MeshIO)

C# binding for [Manifold](https://github.com/elalish/manifold).

Manifold is a geometry library dedicated to creating and operating on manifold triangle meshes. A manifold mesh is a mesh that represents a solid object, and so is very important in manufacturing, CAD, structural analysis, etc. 

# Nuget package 📦

## Manifold

Default package that cannot export glb file!

![NuGet Version](https://img.shields.io/nuget/v/ManifoldNET)

## Manifold.MeshIO

With export options.

![NuGet Version](https://img.shields.io/nuget/v/ManifoldNET.MeshIO)

# Quick start 🚀

> dotnet add package ManifoldNET.MeshIO

```c#
using ManifoldNET;

Manifold cube = Manifold.Cube(1, 1, 1, true);
```

# Notebooks 📓

> You can quickly experience the usage of ManifoldNET through notebooks.

* [01.GetStarted.ipynb](./bindings/csharp/notebooks/01.GetStarted.ipynb)

> Requirements: C# notebooks require .NET 8 and the VS Code Polyglot extension.

# References 🔗

[Manifold](https://github.com/elalish/manifold).

[P/Invoke](https://learn.microsoft.com/zh-cn/dotnet/standard/native-interop/)
