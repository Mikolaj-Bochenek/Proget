namespace MicroSender.Core.Domain.Entities;

public class MicroSenderMessage
{
    public Guid Id { get; private set; }
    public int Code { get; private set; }
    public string? Name { get; private set; }

    public MicroSenderMessage(Guid id, int code, string? name)
    {
        Id = id;
        Code = code;
        Name = name;
    }
}
