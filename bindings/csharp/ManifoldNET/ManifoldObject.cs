namespace ManifoldNET;

/// <summary>
/// Base object for manifold.
/// </summary>
/// <remarks>
/// Initializes a base object.
/// </remarks>
public abstract class ManifoldObject : IDisposable
{
    /// <summary>
    /// Pointer for this object.
    /// </summary>
    protected IntPtr _pointer = IntPtr.Zero;
    private bool _disposedValue;

    /// <param name="pointer"></param>
    protected ManifoldObject(IntPtr pointer)
    {
        _pointer = pointer;
    }

    internal IntPtr GetPointer() => _pointer;

    private void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (_pointer != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(_pointer);
                _pointer = IntPtr.Zero;
            }
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

    /// <summary>
    /// Delete by pointer.
    /// </summary>
    /// <param name="pointer"></param>
    protected abstract void Delete(IntPtr pointer);
}
