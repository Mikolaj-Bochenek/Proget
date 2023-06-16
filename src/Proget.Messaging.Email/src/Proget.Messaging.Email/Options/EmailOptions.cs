namespace Proget.Messaging.Email.Options;

public sealed class EmailOptions
{
    public bool SmtpEnabled { get; set; }
    public bool SendGridEnabled { get; set; }
    public string? SenderAddress { get; set; }
    public string? SenderName { get; set; }
}
