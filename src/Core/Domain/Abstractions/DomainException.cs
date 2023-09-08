using Domain.Abstractions.Messages;
using Version = Domain.ValueObjects.Version;

namespace Domain.Abstractions;

public abstract class DomainException<TException>(string message) : InvalidOperationException(message)
    where TException : DomainException<TException>, new()
{
    public static void ThrowIf(bool condition)
    {
        if (condition) throw new TException();
    }

    public static void ThrowIfNull<T>(T t)
    {
        if (t is null) throw new TException();
    }

    public static void Throw()
        => throw new TException();

    public static IDomainEvent Throw(Version _)
        => throw new TException();
}