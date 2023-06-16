namespace Proget.ORM.EntityFramework.Exceptions;

internal sealed class MissingConnectionStringException : Exception
{
    public HttpStatusCode StatusCode = HttpStatusCode.BadRequest;

    public MissingConnectionStringException()
        : base($"The connection string in Proget.ORM.EntityFramework options is missing."
            + $" Add proper connection string option according to the Proget documentation")
    { }
}
