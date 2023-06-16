namespace MicroRecipient.Core.Domain.Entities;

public class RecipientMessage
{
    public Guid Id { get; private set; }
    public int Code { get; private set; }
    public string? Name { get; private set; }

    public RecipientMessage(Guid id, int code, string? name)
    {
        Id = id;
        Code = code;
        Name = name;
    }
}
