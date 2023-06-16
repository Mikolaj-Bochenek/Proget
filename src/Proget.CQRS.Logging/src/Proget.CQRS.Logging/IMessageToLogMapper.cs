using Proget.CQRS.Logging.Templates;

namespace Proget.CQRS.Logging;

public interface ILoggingMapper
{
    LogTemplate? Map<TMessage>(TMessage message) where TMessage : class;
}
