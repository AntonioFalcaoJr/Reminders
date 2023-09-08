using Domain.Abstractions.Messages;

namespace Domain.Aggregates.Events;

public static class DelayedEvent
{
    public record ReminderElapsed(string ReminderId, string Address) : Message, IDelayedEvent;
}