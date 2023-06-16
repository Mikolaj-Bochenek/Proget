namespace Proget.Auth.Exceptions;

[DataContract]
internal sealed class UnauthorizedException : Exception
{
    [DataMember]
    public HttpStatusCode StatusCode = HttpStatusCode.Unauthorized;

    [DataMember]
    public override string Message { get; }

    public UnauthorizedException(string message = "Access denied") 
        : base(message) => Message = message;
}
