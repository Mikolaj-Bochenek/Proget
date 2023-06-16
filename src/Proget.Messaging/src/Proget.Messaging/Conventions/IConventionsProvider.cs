namespace Proget.Messaging.Conventions;

public interface IConventionsProvider
{
    IMessageConventions Get<TMessage>() where TMessage : class, IMessage;
    IMessageConventions Get(Type type);
}
