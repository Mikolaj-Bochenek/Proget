namespace Proget.ORM;

public interface IIdentifier<out T>
{
    T Id { get; }
}
