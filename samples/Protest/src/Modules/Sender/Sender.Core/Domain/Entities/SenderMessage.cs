namespace Sender.Core.Domain.Entities;

public class SenderMessage
{
    public Guid Id { get; private set; }
    public int Code { get; private set; }
    public string? Name { get; private set; }

    public SenderMessage(Guid id, int code, string? name)
    {
        Id = id;
        Code = code;
        Name = name;
    }
}
