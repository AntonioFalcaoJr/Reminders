using Domain.Abstractions.Aggregates;
using Domain.Abstractions.Messages;
using Domain.Aggregates.Events;
using Domain.Enumerations;
using Domain.ValueObjects;
using static Domain.Exceptions;
using Timer = Domain.ValueObjects.Timer;

namespace Domain.Aggregates;

public class Reminder : AggregateRoot<ReminderId>
{
    public Timer Timer { get; private set; } = Timer.Initial;
    public Address Address { get; private set; } = Address.Undefined;
    public ReminderStatus Status { get; private set; } = ReminderStatus.Available;
    public Time ScheduledTime { get; private set; } = Time.Zero;

    public TimeSpan TimeLeft
    {
        get
        {
            var remainingTime = ScheduledTime - Time.Now;
            return remainingTime < TimeSpan.Zero ? TimeSpan.Zero : remainingTime;
        }
    }

    public static Reminder Define(Timer timer, Address address)
    {
        Reminder reminder = new();
        var scheduledTime = Time.Now.Add(timer.Hours, timer.Minutes, timer.Seconds);
        reminder.OnDefine(timer, address, scheduledTime);
        return reminder;
    }

    private void OnDefine(Timer timer, Address address, Time scheduledTime)
    {
        if (Status != ReminderStatus.Available) ReminderAlreadyDefined.Throw();
        RaiseEvent<DomainEvent.ReminderDefined>(version
            => new(Id, timer, address, scheduledTime, ReminderStatus.Active, version));
    }

    public void MarkAsCompleted()
    {
        if (Status != ReminderStatus.Active) ReminderNotActive.Throw();
        RaiseEvent<DomainEvent.ReminderCompleted>(version => new(Id, ReminderStatus.Completed, version));
    }

    public void MarkAsFailed()
    {
        if (Status != ReminderStatus.Active) ReminderNotActive.Throw();
        RaiseEvent<DomainEvent.ReminderCompleted>(version => new(Id, ReminderStatus.Failed, version));
    }

    protected override void Apply(IDomainEvent @event)
        => When(@event as dynamic);

    private void When(DomainEvent.ReminderDefined @event)
    {
        Id = (ReminderId)@event.ReminderId;
        Timer = (Timer)@event.Timer;
        Address = (Address)@event.Address;
        Status = (ReminderStatus)@event.Status;
        ScheduledTime = (Time)@event.ScheduledTime;
    }

    private void When(DomainEvent.ReminderCompleted @event)
        => Status = (ReminderStatus)@event.Status;

    private void When(DomainEvent.ReminderFailed @event)
        => Status = (ReminderStatus)@event.Status;
}