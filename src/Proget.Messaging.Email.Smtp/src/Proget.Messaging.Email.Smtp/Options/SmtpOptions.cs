namespace Proget.Messaging.Email.Smtp.Options;

public sealed class SmtpOptions
{
    public bool SslEnabled { get; set; } = true;
    public int Port { get; set; }
    public string? Host { get; set; }
    public string? Password { get; set; }
}
