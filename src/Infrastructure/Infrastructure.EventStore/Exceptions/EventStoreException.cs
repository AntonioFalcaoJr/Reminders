namespace Infrastructure.EventStore.Exceptions;

public abstract class EventStoreException<TException>(string message) : InvalidOperationException(message)
    where TException : EventStoreException<TException>, new()
{
    public static void ThrowIf(bool condition)
    {
        if (condition) throw new TException();
    }
}