namespace Proget.CQRS.Events;

public interface IRejectedEvent : IEvent
{
    string Reason { get; }
    string Code { get; }
    int StatusCode { get; }
}
