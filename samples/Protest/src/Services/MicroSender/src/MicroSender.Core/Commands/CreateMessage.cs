namespace MicroSender.Core.Commands;

public sealed record CreateMessage(Guid Id, int Code, string Name) : ICommand;
