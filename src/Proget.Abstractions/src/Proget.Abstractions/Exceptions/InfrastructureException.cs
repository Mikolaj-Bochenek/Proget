using System.Net;

namespace Proget.Abstractions.Exceptions;

public abstract class InfrastructureException : Exception
{
    public abstract HttpStatusCode StatusCode { get; }
    protected InfrastructureException(string message) : base(message)
    {
    }
}
