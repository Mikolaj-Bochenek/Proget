namespace Proget.WebAPI.Exceptions;

public interface IExceptionMapper
{
    ExceptionResponse Map(Exception exception);
}
