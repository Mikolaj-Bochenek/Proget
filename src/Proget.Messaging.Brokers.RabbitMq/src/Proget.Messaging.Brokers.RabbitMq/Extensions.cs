namespace Proget.Messaging.Brokers.RabbitMq;

public static class Extensions
{
    private const string Section = "messaging:brokers:rabbitmq";

    public static IMessagingConfigurator AddRabbitMq(this IMessagingConfigurator configurator,
        Func<IRabbitMqOptionsBuilder, IRabbitMqOptionsBuilder>? optionsBuilder = null, bool enabled = true, string section = Section)
    {   
        var builder = configurator.Builder;

        var options = builder.ConfigureOptions<RabbitMqOptions, RabbitMqOptionsBuilder, IRabbitMqOptionsBuilder>(section ?? Section, optionsBuilder);

        CreateRabbitMqConnection();
        
        builder.Services.AddSingleton<ChannelAccessor>();
        builder.Services.AddSingleton<IChannelFactory, ChannelFactory>();
        builder.Services.AddSingleton<IBrokerPublisher, RabbitMqPublisher>();
        
        builder.Services.AddSingleton<IConventionsBuilder, RabbitMqConventionsBuilder>();
        builder.Services.AddSingleton<IConventionsProvider, RabbitMqConventionsProvider>();

        builder.Services.AddSingleton<IBrokerSubscriber, RabbitMqSubscriber>();

        configurator.Options.BrokerEnabled = enabled;
      
        return configurator;

        void CreateRabbitMqConnection()
        {
            var factory = new ConnectionFactory
            {
                HostName = options.Connection.HostName,
                VirtualHost = options.Connection.VirtualHost,
                Port = options.Connection.Port,
                UserName = options.Connection.UserName,
                Password = options.Connection.Password
            };

            var connection = factory.CreateConnection();
            builder.Services.AddSingleton(connection);
        }
    }
}

