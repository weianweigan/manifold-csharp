using System;

namespace ManifoldNET.Notebooks;

[Serializable]
public class ManifoldNotebookException : Exception
{
    public ManifoldNotebookException() { }

    public ManifoldNotebookException(string message)
        : base(message) { }

    public ManifoldNotebookException(string message, Exception inner)
        : base(message, inner) { }
}
