using System.Net;

namespace Proget.Abstractions.Exceptions;

public abstract class DomainException : Exception
{
    public abstract HttpStatusCode StatusCode { get; }
    protected DomainException(string message) : base(message)
    {
    }
}