namespace Proget.Messaging.Email.Smtp.Exceptions;

internal class InvalidSmtpOptionsException : Exception
{
    private const string? ErrorMessage = "The Proget.Messaging.Email.Smtp configuration is missing."
        + " To work with this package properly add the defualt 'messaging:mailing:smtp' section in appsettings.json"
        + " or use OptionsBuilder via lambda expression in Program.cs";
    public InvalidSmtpOptionsException() : base(ErrorMessage) { }
}
