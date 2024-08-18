using System.Collections;
using System.Collections.Generic;
using static ManifoldNET.Import.Native;

namespace ManifoldNET;

/// <summary>
/// Manifold list.
/// Usage: <see cref="Manifold.BatchBoolOperation(ManifoldArray, BoolOperationType)"/>
/// </summary>
internal sealed class ManifoldArray : ManifoldObject, IEnumerable<Manifold>
{
    sealed class ManifoldsIterator : IEnumerator<Manifold>
    {
        private ManifoldArray _manifolds;
        private bool _disposedValue;

        public int Position { get; set; }

        public ManifoldsIterator(ManifoldArray manifolds)
        {
            this._manifolds = manifolds;
            this.Position = this._manifolds.Length;
        }

        public object Current => Current;

        Manifold IEnumerator<Manifold>.Current
        {
            get
            {
                if (this.Position == -1 || this.Position == this._manifolds.Length)
                {
                    throw new InvalidOperationException();
                }
                return this._manifolds[this.Position];
            }
        }

        public bool MoveNext()
        {
            if (this.Position != -1)
            {
                this.Position--;
            }
            return this.Position > -1;
        }

        public void Reset()
        {
            this.Position = this._manifolds.Length;
        }

        private void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
#pragma warning disable CS8625
                    _manifolds = null;
#pragma warning restore CS8625
                }

                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }

    internal ManifoldArray(IntPtr intPtr)
        : base(intPtr)
    {
        _pointer = intPtr;
    }

    internal ManifoldArray()
        : base(manifold_manifold_empty_vec(Marshal.AllocHGlobal((int)manifold_manifold_vec_size())))
    { }

    internal ManifoldArray(int size)
        : base(
            manifold_manifold_vec(
                Marshal.AllocHGlobal((int)manifold_manifold_vec_size()),
                (ulong)size
            )
        ) { }

    /// <summary>
    /// Create <see cref="ManifoldArray"/> from a list.
    /// </summary>
    /// <param name="manifolds"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public ManifoldArray(IReadOnlyList<Manifold> manifolds)
        : this(manifolds.Count)
    {
        for (int i = 0; i < manifolds.Count; i++)
        {
            if (manifolds[i] == null)
            {
                throw new ArgumentNullException($"manifolds[{i}] Cannot be null!");
            }
            this[i] = manifolds[i];
        }
    }

    /// <summary>
    /// Gets <see cref="Manifold"/> by index.
    /// </summary>
    /// <param name="index">Index.</param>
    public Manifold this[int index]
    {
        get
        {
            ulong size = manifold_manifold_vec_size();
            IntPtr mem = Marshal.AllocHGlobal((int)size);
            IntPtr manifoldPointer = manifold_manifold_vec_get(mem, _pointer, (ulong)index);
            return new Manifold(manifoldPointer);
        }
        set => manifold_manifold_vec_set(_pointer, index, value.GetPointer());
    }

    /// <summary>
    /// The number of <see cref="Manifold"/>.
    /// </summary>
    public int Length => (int)manifold_manifold_vec_length(_pointer);

    /// <summary>
    /// Not work? How to use?
    /// </summary>
    internal void Reverse() => manifold_manifold_vec_reserve(_pointer, (ulong)Length);

    /// <inheritdoc/>
    public IEnumerator<Manifold> GetEnumerator()
    {
        return new ManifoldsIterator(this);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    ///<inheritdoc/>
    protected override void Delete(IntPtr pointer) => manifold_delete_manifold_vec(pointer);
}
