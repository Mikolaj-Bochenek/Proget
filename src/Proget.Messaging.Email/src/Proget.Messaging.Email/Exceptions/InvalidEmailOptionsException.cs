namespace Proget.Messaging.Email.Exceptions;

internal class InvalidEmailOptionsException : Exception
{
    private const string? ErrorMessage = "The Proget.Messaging.Email configuration is missing."
        + " To work with this package properly add the defualt 'messaging:mailing' section in appsettings.json"
        + " or use OptionsBuilder via lambda expression in Program.cs";

    public InvalidEmailOptionsException() : base(ErrorMessage) { }
}