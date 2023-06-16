using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Proget.Attributes;
using Proget.CQRS.Commands;
using Proget.CQRS.Logging.Templates;
using SmartFormat;

namespace Proget.CQRS.Logging.Decorators;

[Decorator]
internal sealed class CommandHandlerLoggingDecorator<TCommand> : ICommandHandler<TCommand>
    where TCommand : class, ICommand
{
    private readonly ICommandHandler<TCommand> _handler;
    private readonly ILogger<CommandHandlerLoggingDecorator<TCommand>> _logger;
    private readonly ILoggingMapper _mapper;

    public CommandHandlerLoggingDecorator(ICommandHandler<TCommand> handler,
        ILogger<CommandHandlerLoggingDecorator<TCommand>> logger, IServiceProvider serviceProvider)
    {
        _handler = handler;
        _logger = logger;
        _mapper = serviceProvider.GetService<ILoggingMapper>() ?? new CommandLoggingMapper();
    }

    public async Task HandleAsync(TCommand command, CancellationToken cancellationToken = default)
    {
        var template = _mapper.Map(command);

        if (template is null)
        {
            Log(command, DefaultCommandLogTemplate.Before(command.GetType()));
            await _handler.HandleAsync(command, cancellationToken);
            Log(command, DefaultCommandLogTemplate.After(command.GetType()));
            return;
        }

        try
        {
            Log(command, template.Before);
            await _handler.HandleAsync(command, cancellationToken);
            Log(command, template.After);
        }
        catch (Exception exception)
        {
            var exceptionTemplate = template.GetExceptionTemplate(exception) ?? exception.Message;

            Log(command, exceptionTemplate, isError: true);
            throw;
        }
    }

    private void Log(TCommand command, string? message, bool isError = false)
    {
        if (string.IsNullOrEmpty(message))
            return;

        if (isError)
            _logger.LogError(Smart.Format(message, command));

        else
            _logger.LogInformation(Smart.Format(message, command));
    }

    private class DefaultCommandLogTemplate
    {
        public static string Before(Type type) => $"Processing an command: {type}";
        public static string After(Type type) => $"Processed an command: {type}";
    }

    private class CommandLoggingMapper : ILoggingMapper
    {
        public LogTemplate? Map<TMessage>(TMessage message) where TMessage : class
            => new LogTemplate
            {
                Before = DefaultCommandLogTemplate.Before(message.GetType()),
                After = DefaultCommandLogTemplate.After(message.GetType()),
            };
    }
}
