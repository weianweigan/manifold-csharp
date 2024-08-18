# How to use

First, you need to install VS Code and the [Polyglot Notebook extension](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.dotnet-interactive-vscode).
Then creat a *.ipynb file to start.

## Install package

```c#
#r "nuget: ManifoldNET.Notebooks, 1.0.4-alpha"
```

## Import namespace

```c#
using ManifoldNET;
```

## Creat geometry elements

```c#
Manifold.Cube(1, 1, 1, true);
```

# Notebook extension

There is some ways to display manifold object:

1. `Manifold.DisplayGlbFile()`;
2. Directly dispaly `Manifold` object;
3. By magic command to display a glb file: `#!glb -f c:/path/to/name.glb`

# Samples

* [01.GetStarted.ipynb](./../notebooks/01.GetStarted.ipynb)

> Requirements: C# notebooks require .NET 8 and the VS Code Polyglot extension.