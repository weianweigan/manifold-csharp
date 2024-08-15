using ManifoldNET.Notebooks;
using Microsoft.DotNet.Interactive;
using System;

namespace ManifoldNET;

public static class Extension
{
    public static void DisplayGlbFile(this Manifold manifold)
    {
        KernelInvocationContext.Current?.DisplayAs(manifold.ToModelViewer().ToString(), "text/html");
    }

    public static void DisplayGlbFile(this string tempFile)
    {
        KernelInvocationContext.Current?.DisplayAs(ManifoldKernelExtension.GenerateGlbHtml(tempFile).ToString(), "text/html");
    }
}
