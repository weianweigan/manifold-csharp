#if MESH_EXPORT
using System.Linq;
using static ManifoldNET.Import.Native;

namespace ManifoldNET;

/// <summary>
/// Export or import file that assimp supports.
/// </summary>
public static class MeshIO
{
    /// <summary>
    /// Saves the Mesh to the desired file type, determined from the extension
    /// specified.In the case of.glb/.gltf, this will save in version 2.0.
    /// </summary>
    /// <remarks>
    /// This is a very simple export function and is intended primarily as a
    /// demonstration.Generally users of this library will need to modify this to
    /// write all the important properties for their application and read any custom
    /// data structures.
    /// </remarks>
    /// <param name="meshGL">Meshgl.</param>
    /// <param name="fileName">
    /// const char* => Ansi charset!
    /// The file extension must be one that Assimp supports for
    /// export.GLB and 3MF are recommended.
    /// </param>
    /// <param name="exportOptions">The options currently only affect an exported GLB's material.</param>
    public static void ExportMeshGL(
        this MeshGL meshGL,
        string fileName,
        ExportOptions? exportOptions = null
    )
    {
        if (ContainsUnicodeCharacter(ref fileName))
        {
            throw new ArgumentException($"{nameof(fileName)} only support ANSI charset");
        }

        manifold_export_meshgl(
            Marshal.StringToHGlobalAnsi(fileName),
            meshGL.GetPointer(),
            (exportOptions ?? new()).GetPointer()
        );
    }

    /// <summary>
    /// Imports the given file as a Mesh structure, which can be converted to a
    /// Manifold if the mesh is a proper oriented 2-manifold.Any supported polygon
    /// format will be automatically triangulated.
    /// </summary>
    /// <remarks>
    /// This is a very simple import function and is intended primarily as a
    /// demonstration.Generally users of this library will need to modify this to
    /// read all the important properties for their application and set up any custom
    /// data structures.
    /// </remarks>
    /// <param name="fileName">
    /// const char* => Ansi charset!
    /// Supports any format the Assimp library supports.
    /// </param>
    /// <param name="forceCleanUp">
    /// This merges identical vertices, which can break
    /// manifoldness.However it is always done for STLs, as they cannot possibly be
    /// manifold without this step.
    /// </param>
    /// <returns></returns>
    public static MeshGL ImportMeshGL(string fileName, bool forceCleanUp)
    {
        if (ContainsUnicodeCharacter(ref fileName))
        {
            throw new ArgumentException($"{nameof(fileName)} only support ANSI charset");
        }

        return new MeshGL(
            manifold_import_meshgl(
                Marshal.AllocHGlobal((int)manifold_meshgl_size()),
                Marshal.StringToHGlobalAnsi(fileName),
                forceCleanUp ? 1 : 0
            ),
            cOwner: true
        );
    }

    internal static bool ContainsUnicodeCharacter(ref string input) => input.Any(c => c > 255);
}
#endif
