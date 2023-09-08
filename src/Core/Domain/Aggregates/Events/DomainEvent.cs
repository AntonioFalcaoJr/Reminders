using Domain.Abstractions.Messages;

namespace Domain.Aggregates.Events;

public static class DomainEvent
{
    public record ReminderDefined(string ReminderId, string Timer, string Address, string ScheduledTime,
        string Status, string Version) : Message, IDomainEvent;

    public record ReminderCompleted(string ReminderId, string Status, string Version) : Message, IDomainEvent;

    public record ReminderFailed(string ReminderId, string Status, string Version) : Message, IDomainEvent;
}