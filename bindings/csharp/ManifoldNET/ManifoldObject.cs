using System.Runtime.InteropServices;

namespace ManifoldNET;

/// <summary>
/// Base object for manifold library.
/// </summary>
/// <remarks>
/// Initializess base object.
/// </remarks>
/// <param name="pointer"></param>
public abstract class ManifoldObject(nint pointer) : IDisposable
{
    /// <summary>
    /// Pointer for this object.
    /// </summary>
    protected nint _pointer = pointer;
    private bool _disposedValue;

    ///<inheritdoc/>
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            Marshal.FreeHGlobal(_pointer);
            _disposedValue = true;
        }
    }

    ///<inheritdoc/>
    ~ManifoldObject()
    {
        Dispose(disposing: false);
    }

    ///<inheritdoc/>
    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
