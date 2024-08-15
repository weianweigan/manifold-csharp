using System;
using System.CommandLine;
using System.IO;
using System.Text;
using Microsoft.DotNet.Interactive;
using Microsoft.DotNet.Interactive.Formatting;
using static Microsoft.DotNet.Interactive.Formatting.PocketViewTags;

namespace ManifoldNET.Notebooks;

public static class ManifoldKernelExtension
{
    public static void Load(Kernel kernel)
    {
        // Extension for Manifold object.
        Formatter.Register<Manifold>(
            (m, writer) => writer.Write(m.ToModelViewer().ToString()),
            "text/html"
        );

        // Next, define a magic command that will display glb file.
        Command glbCommand = CreateGlbMagicCommand();

        kernel.AddDirective(glbCommand);

        // Finally, display some information to the user so they can see how to use the extension.
        PocketView view = div(
            code("ManifoldNET.Notebooks"),
            " is loaded. It adds visualizations for ",
            code(typeof(Manifold)),
            ". Try it by running: ",
            code("Manifold.Cube(1, 1, 1, true)"),
            " or using magic code to disply glb file:",
            code("#!glb -f c:/path/to/name.glb")
        );

        KernelInvocationContext.Current?.Display(view);
    }

    private static Command CreateGlbMagicCommand()
    {
        var fileOption = new Option<string>(["-f", "--file"], "The glb file local postion.");

        var clockCommand = new Command("#!glb", "Displays a glb file from disk.") { fileOption, };

        clockCommand.SetHandler(
            (f) =>
                KernelInvocationContext.Current.DisplayAs(
                    GenerateGlbHtml(f).ToString(),
                    "text/html"
                ),
            fileOption
        );
        return clockCommand;
    }

    public static StringBuilder ToModelViewer(this Manifold manifold)
    {
        string tempFile = Path.GetTempFileName();

        try
        {
#if MESH_EXPORT
            manifold.MeshGL.ExportMeshGL(tempFile);
            return GenerateGlbHtml(tempFile);
#else
            throw new ManifoldNotebookException(
                "Cannot export glb file, because Export option did not on!"
            );
#endif
        }
        catch (Exception ex)
        {
            throw new ManifoldNotebookException(tempFile, ex);
        }
        finally
        {
            if (File.Exists(tempFile))
            {
                File.Delete(tempFile);
            }
        }
    }

    private static StringBuilder GenerateGlbHtml(string tempFile)
    {
        StringBuilder sb = new();
        byte[] fileBytes = File.ReadAllBytes(tempFile);
        string modelData = Convert.ToBase64String(fileBytes);

        sb.Append(
            """
            <!DOCTYPE html>
            <html lang="en">
            <head>
                <meta charset="UTF-8">
                <meta name="viewport" content="width=device-width, initial-scale=1.0">
                <script type="module" src="https://ajax.googleapis.com/ajax/libs/model-viewer/3.5.0/model-viewer.min.js"></script>
                <script src="https://cdn.jsdelivr.net/npm/js-base64@3.6.1/base64.min.js"></script>
                <style>
                    html, body {
                        height: 100%;
                        margin: 0;
                        padding: 0;
                    }

                    model-viewer {
                        width: 100%;
                        height: 500px;
                    }
                </style>
            </head>
            <body>
                <model-viewer id="modelViewer"
                    autoplay
                    alt="3D Model"
                    shadow-intensity="1" camera-controls
                    touch-action="pan-y" src="data:image/jpeg;base64, 
            """
        );
        sb.Append(modelData);
        sb.Append(
            """
            "></model-viewer>
            </body>
            </html>
            """
        );

        return sb;
    }
}
