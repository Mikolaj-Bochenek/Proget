namespace Proget.Messaging.InMemory.Eventing;

internal delegate Task AsyncEventHandler<in TEvent>(TEvent @event) where TEvent : EventArgs;
