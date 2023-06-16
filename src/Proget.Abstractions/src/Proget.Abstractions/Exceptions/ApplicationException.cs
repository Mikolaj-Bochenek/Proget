using System.Net;

namespace Proget.Abstractions.Exceptions;

public abstract class ApplicationException : Exception
{
    public abstract HttpStatusCode StatusCode { get; }
    protected ApplicationException(string message) : base(message)
    {
    }
}
