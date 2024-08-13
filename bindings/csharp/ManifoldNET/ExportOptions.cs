#if MESH_EXPORT
using static ManifoldNET.Import.Native;

namespace ManifoldNET;

/// <summary>
/// These options only currently affect .glb and .gltf files.
/// </summary>
public class ExportOptions : ManifoldObject
{
    private bool _faceted = true;
    private Material _material;

    /// <summary>
    /// When false, vertex normals are exported, causing the mesh to appear smooth
    /// through normal interpolation.
    /// </summary>
    public bool Faceted
    {
        get => _faceted;
        set
        {
            _faceted = value;
            manifold_export_options_set_faceted(_pointer, value ? 1 : 0);
        }
    }

    /// <summary>
    /// PBR material properties.
    /// </summary>
    public Material Material
    {
        get => _material;
        set
        {
            _material = value;
            manifold_export_options_set_material(_pointer, value.GetPointer());
        }
    }

    /// <summary>
    /// Initializes a new instance of <see cref="ExportOptions"/> struct.
    /// </summary>
    public ExportOptions()
        : base(manifold_export_options(Marshal.AllocHGlobal((int)manifold_material_size())))
    {
        _material = new Material();
        manifold_export_options_set_material(_pointer, _material.GetPointer());
    }

    ///<inheritdoc/>
    protected override void Delete(IntPtr pointer) => manifold_delete_export_options(pointer);
}
#else
namespace ManifoldNET;

/// <summary>
/// Represents the options for exporting 3D models.
/// </summary>
public sealed class ExportOptions
{
    /// <summary>
    /// Gets or sets a value indicating whether the exported model should be faceted.
    /// Faceted models have sharp edges between faces, as opposed to smooth shading.
    /// </summary>
    /// <value>
    /// <c>true</c> if the model should be faceted; otherwise, <c>false</c>.
    /// </value>
    public bool Faceted { get; set; } = true;

    /// <summary>
    /// Gets or sets the material to be used for the exported model.
    /// </summary>
    /// <value>
    /// An instance of the <see cref="Material"/> class representing the material properties.
    /// </value>
    public Material Material { get; set; } = new();
}
#endif
