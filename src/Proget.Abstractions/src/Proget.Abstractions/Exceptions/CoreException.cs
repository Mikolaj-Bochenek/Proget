using System.Net;

namespace Proget.Abstractions.Exceptions;

public abstract class CoreException : Exception
{
    public abstract HttpStatusCode StatusCode { get; }
    protected CoreException(string message) : base(message)
    {
    }
}
