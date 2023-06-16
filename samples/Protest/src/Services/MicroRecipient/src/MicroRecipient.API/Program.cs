using MicroRecipient.Core.Events.External;
using Proget.CQRS.Events;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddProget(builder.Configuration, "proget")
    .AddCore(builder.Configuration)
    .AddEvents()
    .AddQueries()
    .AddMessaging(media => media
        .AddRabbitMq(options => options
            .SetLogger()
            .SetConnection(connection => connection
                .SetHostName("localhost")
                .SetPort(5672)
                .SetPassword("R46617mQ!")
                .SetUserName("rabbitmq")
                .SetVirtualHost("/"))
            .SetQueue(queue => queue
                .SetAutoDelete(false)
                .SetDeclare()
                .SetExclusive()
                .SetDurable()
                .SetTemplate("{{assembly}}/{{exchange}}.{{message}}"))
            .SetMessagePersistence()
            .SetConventions(conventions => conventions
                .IgnoreExchangeAttachment(false)
                .IgnoreQueueAttachment(false)
                .IgnoreRoutingKeyAttachment(false))
            .SetAck(ack => ack
                .SetMultipleAck(false)
                .SetMultipleNack(false))
            .SetQos(qos => qos
                .SetGlobal(false)
                .SetPrefetchCount(2)
                .SetPrefetchSize(0)),
            true, "messaging:brokers:rabbitmq"));

var app = builder.Build();

app.UseMessaging()
    .Subscribe<SenderMessageCreated>(async (serviceProvider, message) => {
        using var scope = serviceProvider.CreateScope();
        await scope.ServiceProvider.GetRequiredService<IEventHandler<SenderMessageCreated>>().HandleAsync(message);
    });

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();




