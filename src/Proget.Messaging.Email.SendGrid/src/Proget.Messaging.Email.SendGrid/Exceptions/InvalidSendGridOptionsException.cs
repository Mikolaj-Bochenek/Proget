namespace Proget.Messaging.Email.SendGrid.Exceptions;

internal class InvalidSendGridOptionsException : Exception
{
    private const string? ErrorMessage = "The Proget.Messaging.Email.SendGrid configuration is missing."
        + " To work with this package properly add the defualt 'messaging:mailing:sendgrid' section in appsettings.json"
        + " or use OptionsBuilder via lambda expression in Program.cs";
    public InvalidSendGridOptionsException() : base(ErrorMessage) { }
}
