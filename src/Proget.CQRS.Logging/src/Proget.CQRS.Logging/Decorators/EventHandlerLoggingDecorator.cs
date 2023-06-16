using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Proget.Attributes;
using Proget.CQRS.Events;
using Proget.CQRS.Logging.Templates;
using SmartFormat;

namespace Proget.CQRS.Logging.Decorators;

[Decorator]
internal sealed class EventHandlerLoggingDecorator<TEvent> : IEventHandler<TEvent>
    where TEvent : class, IEvent
{
    private readonly IEventHandler<TEvent> _handler;
    private readonly ILogger<EventHandlerLoggingDecorator<TEvent>> _logger;
    private readonly ILoggingMapper _mapper;

    public EventHandlerLoggingDecorator(IEventHandler<TEvent> handler,
        ILogger<EventHandlerLoggingDecorator<TEvent>> logger, IServiceProvider serviceProvider)
    {
        _handler = handler;
        _logger = logger;
        _mapper = serviceProvider.GetService<ILoggingMapper>() ?? new EventLoggingMapper();
    }

    public async Task HandleAsync(TEvent @event, CancellationToken cancellationToken = default)
    {
        var template = _mapper.Map(@event);

        if (template is null)
        {
            Log(@event, DefaultEventLogTemplate.Before(@event.GetType()));
            await _handler.HandleAsync(@event, cancellationToken);
            Log(@event, DefaultEventLogTemplate.After(@event.GetType()));
            return;
        }

        try
        {
            Log(@event, template.Before);
            await _handler.HandleAsync(@event, cancellationToken);
            Log(@event, template.After);
        }
        catch (Exception exception)
        {
            var exceptionTemplate = template.GetExceptionTemplate(exception) ?? exception.Message;

            Log(@event, exceptionTemplate, isError: true);
            throw;
        }
    }

    private void Log(TEvent @event, string? message, bool isError = false)
    {
        if (string.IsNullOrEmpty(message))
            return;

        if (isError)
            _logger.LogError(Smart.Format(message, @event));

        else
            _logger.LogInformation(Smart.Format(message, @event));
    }

    private class DefaultEventLogTemplate
    {
        public static string Before(Type type) => $"Processing an event: {type}";
        public static string After(Type type) => $"Processed an event: {type}";
    }

    private class EventLoggingMapper : ILoggingMapper
    {
        public LogTemplate? Map<TMessage>(TMessage message) where TMessage : class
            => new LogTemplate
            {
                Before = DefaultEventLogTemplate.Before(message.GetType()),
                After = DefaultEventLogTemplate.After(message.GetType()),
            };
    }
}
