namespace Proget.Messaging.Brokers.RabbitMq.Options.Builders;

public sealed class ConventionsOptionsBuilder : IConventionsOptionsBuilder
{
    private readonly ConventionsOptions _conventionsOptions = new();

    public ConventionsOptions Build()
        => _conventionsOptions;

    public IConventionsOptionsBuilder IgnoreExchangeAttachment(bool value = true)
    {
        _conventionsOptions.IgnoreExchangeAttribute = value;
        return this;
    }

    public IConventionsOptionsBuilder IgnoreQueueAttachment(bool value = true)
    {
        _conventionsOptions.IgnoreQueueAttribute = value;
        return this;
    }

    public IConventionsOptionsBuilder IgnoreRoutingKeyAttachment(bool value = true)
    {
        _conventionsOptions.IgnoreRoutingKeyAttribute = value;
        return this;
    }

    public IConventionsOptionsBuilder SetCasing(string? value)
    {
        _conventionsOptions.Casing = TryParseCasingType();
        return this;

        string? TryParseCasingType()
        {
            if (string.Equals(CasingType.CamelCase, value, StringComparison.InvariantCultureIgnoreCase))
                return CasingType.CamelCase;
            else if (string.Equals(CasingType.PascalCase, value, StringComparison.InvariantCultureIgnoreCase))
                return CasingType.PascalCase;
            else if (string.Equals(CasingType.SnakeCase, value, StringComparison.InvariantCultureIgnoreCase))
                return CasingType.SnakeCase;
            
            throw new Exception("The type of the casing has to be one of these: ['CamelCase', 'PascalCase', 'SnakeCase']");
        }
    }
}
