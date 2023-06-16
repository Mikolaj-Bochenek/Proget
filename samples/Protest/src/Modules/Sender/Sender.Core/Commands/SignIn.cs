namespace Sender.Core.Commands;

public sealed record SignIn(Guid UserId, string Type, string Claims) : ICommand;
