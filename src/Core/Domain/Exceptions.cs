using Domain.Abstractions;

namespace Domain;

public static class Exceptions
{
    public class InvalidIdentifier() : DomainException<InvalidIdentifier>("Invalid identifier.");

    public class ReminderAlreadyDefined() : DomainException<ReminderAlreadyDefined>("Reminder already defined.");
    public class ReminderNotActive() : DomainException<ReminderNotActive>("Reminder not active.");

    public class TimerCannotBeZero() : DomainException<TimerCannotBeZero>("Timer cannot be zero.");

    public class TimeUnityCannotBeNegative() : DomainException<TimeUnityCannotBeNegative>("A time unity cannot be negative.");

    public class TimeUnityMustBeAPositiveNumber() : DomainException<TimeUnityMustBeAPositiveNumber>("A time unity must be a positive number.");
}