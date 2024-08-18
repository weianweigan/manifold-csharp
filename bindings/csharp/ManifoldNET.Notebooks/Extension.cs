using ManifoldNET.Notebooks;
using Microsoft.DotNet.Interactive;
using System;

namespace ManifoldNET;

public static class Extension
{
    public static void DisplayGlbFile(this Manifold manifold, int height = 400)
    {
        KernelInvocationContext.Current?.DisplayAs(manifold.ToModelViewer(height).ToString(), "text/html");
    }

    public static void DisplayGlbFile(this string tempFile)
    {
        KernelInvocationContext.Current?.DisplayAs(ManifoldKernelExtension.GenerateGlbHtml(tempFile).ToString(), "text/html");
    }
}
