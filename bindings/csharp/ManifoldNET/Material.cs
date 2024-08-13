#if MESH_EXPORT
using static ManifoldNET.Import.Native;

namespace ManifoldNET;

/// <summary>
/// PBR material properties for GLB/glTF files.
/// </summary>
public sealed class Material : ManifoldObject
{
    private float _roughness = 0.2f;
    private float _metalness = 1f;
    private Vector4[]? _vertColor;
    private Vector4 _color = new(1f, 1f, 1f, 1f);

    /// <summary>
    /// Roughness value between 0 (shiny) and 1 (matte)..
    /// </summary>
    public float Roughness
    {
        get => _roughness;
        set
        {
            _roughness = value;
            manifold_material_set_roughness(_pointer, _roughness);
        }
    }

    /// <summary>
    /// Metalness value, generally either 0 (dielectric) or 1 (metal).
    /// </summary>
    public float Metalness
    {
        get => _metalness;
        set
        {
            _metalness = value;
            manifold_material_set_metalness(_pointer, _metalness);
        }
    }

    /// <summary>
    /// Color (RGBA) multiplier to apply to the whole mesh (each value between 0 and 1).
    /// </summary>
    public Vector4 Color
    {
        get => _color;
        set
        {
            _color = value;
            manifold_material_set_color(_pointer, _color);
        }
    }

    /// <summary>
    /// Optional: If non-empty, must match Mesh.vertPos. Provides an RGBA color
    /// for each vertex, linearly interpolated across triangles. Colors are
    /// linear, not sRGB. Only used with Mesh export, not MeshGL.
    /// </summary>
    public Vector4[]? VertColor
    {
        get => _vertColor;
        set
        {
            _vertColor = value;
            if (value == null)
            {
                manifold_material_set_vert_color(_pointer, null, 0);
            }
            else
            {
                manifold_material_set_vert_color(_pointer, value, (ulong)value.Length);
            }
        }
    }

    /// <summary>
    /// For MeshGL export, gives the property indicies where the normal channels
    /// can be found. Must be >= 3, since the first three are position.
    /// </summary>
    private IntVector3 normalChannels { get; set; } = new(-1, -1, -1);

    /// <summary>
    /// For MeshGL export, gives the property indicies where the color channels
    /// can be found. Any index &lt; 0 will output all 1.0 for that channel.
    /// </summary>
    private IntVector3 colorChannels { get; set; } = new(-1, -1, -1);

    /// <summary>
    /// Initializes a new <see cref="Material"/> instance of <see cref="Material"/> struct.
    /// </summary>
    public Material()
        : base(manifold_material(Marshal.AllocHGlobal((int)manifold_material_size()))) { }

    ///<inheritdoc/>
    protected override void Delete(IntPtr pointer) => manifold_delete_material(pointer);
}
#else
namespace ManifoldNET;

/// <summary>
/// Represents the material properties of a 3D object.
/// </summary>
public sealed class Material
{
    /// <summary>
    /// Gets or sets the roughness of the material.
    /// Roughness affects how much light is scattered when it hits the surface.
    /// </summary>
    /// <value>
    /// The roughness value, typically ranging from 0.0 (smooth) to 1.0 (rough).
    /// </value>
    public float Roughness { get; set; } = 0.2f;

    /// <summary>
    /// Gets or sets the metalness of the material.
    /// Metalness determines whether the material behaves like a metal.
    /// </summary>
    /// <value>
    /// The metalness value, ranging from 0.0 (non-metal) to 1.0 (metal).
    /// </value>
    public float Metalness { get; set; } = 1f;

    /// <summary>
    /// Gets or sets the base color of the material.
    /// </summary>
    /// <value>
    /// A <see cref="Vector4"/> representing the RGBA color of the material.
    /// Each component should be in the range [0.0, 1.0].
    /// </value>
    public Vector4 Color { get; set; } = new(1f, 1f, 1f, 1f);

    /// <summary>
    /// Gets or sets the per-vertex colors of the material.
    /// If set, these colors will be used instead of the base color for each vertex.
    /// </summary>
    /// <value>
    /// An array of <see cref="Vector4"/> representing the RGBA colors for each vertex.
    /// Each component should be in the range [0.0, 1.0].
    /// </value>
    public Vector4[]? VertColor { get; set; }
}
#endif
