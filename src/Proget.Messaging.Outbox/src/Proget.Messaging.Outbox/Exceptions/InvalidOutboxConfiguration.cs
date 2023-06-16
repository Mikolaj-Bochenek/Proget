namespace Proget.Messaging.Outbox.Exceptions;

internal sealed class InvalidOutboxConfiguration : Exception
{
    private const string ErrorMessage = "The Proget.Messaging.Oubox configuration is missing.";
    
    public InvalidOutboxConfiguration() : base(ErrorMessage)
    {
    }
}
