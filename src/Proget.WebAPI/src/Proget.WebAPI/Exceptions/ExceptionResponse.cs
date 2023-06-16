namespace Proget.WebAPI.Exceptions;

public record ExceptionResponse(object Response, HttpStatusCode StatusCode);
