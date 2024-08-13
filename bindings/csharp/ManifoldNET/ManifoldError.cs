namespace ManifoldNET;

/// <summary>
/// Manifold status.
/// </summary>
public enum ManifoldError
{
    /// <summary>
    /// No error.
    /// </summary>
    NoError,

    /// <summary>
    /// Non-finite vertex.
    /// </summary>
    NonFiniteVertex,

    /// <summary>
    /// Not manifold.
    /// </summary>
    NotManifold,

    /// <summary>
    /// Vertex index out of bounds.
    /// </summary>
    VertexIndexOutOfBounds,

    /// <summary>
    /// Properties have the wrong length.
    /// </summary>
    PropertiesWrongLength,

    /// <summary>
    /// Less than three properties.
    /// </summary>
    MissingPositionProperties,

    /// <summary>
    /// Merge vectors have different lengths.
    /// </summary>
    MergeVectorsDifferentLengths,

    /// <summary>
    /// Merge index out of bounds.
    /// </summary>
    MergeIndexOutOfBounds,

    /// <summary>
    /// Transform vector has the wrong length.
    /// </summary>
    TransformWrongLength,

    /// <summary>
    /// Run index vector has the wrong length.
    /// </summary>
    RunIndexWrongLength,

    /// <summary>
    /// Face ID vector has the wrong length.
    /// </summary>
    FaceIdWrongLength,

    /// <summary>
    /// Manifold constructed with invalid parameters.
    /// </summary>
    InvalidConstruction,
}
