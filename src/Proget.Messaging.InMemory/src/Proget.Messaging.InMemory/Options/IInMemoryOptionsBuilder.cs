using Proget.Options;

namespace Proget.Messaging.InMemory.Options;

public interface IInMemoryOptionsBuilder : IProgetOptionsBuilder<InMemoryOptions>
{
    IInMemoryOptionsBuilder SetLogger(bool value = true);
    IInMemoryOptionsBuilder SetExchange(string? value);
    IInMemoryOptionsBuilder SetRoutingKey(string? value);
    IInMemoryOptionsBuilder SetIgnoreRoutingKeyAttribute(bool value = true);
}
