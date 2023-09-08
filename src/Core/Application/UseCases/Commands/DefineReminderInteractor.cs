using Application.Abstractions;
using Application.Services;
using Domain.Aggregates;
using Domain.Aggregates.Events;
using Domain.ValueObjects;
using Timer = Domain.ValueObjects.Timer;

namespace Application.UseCases.Commands;

public record DefineReminderCommand(Hours Hours, Minutes Minutes, Seconds Seconds, Address Address);

public record DefineReminderResponse(string Id, string Status, double TimeLeft);

public interface IDefineReminderInteractor : IInteractor<DefineReminderCommand, DefineReminderResponse>;

public class DefineReminderInteractor(IApplicationService service) : IDefineReminderInteractor
{
    public async Task<DefineReminderResponse> InteractAsync(DefineReminderCommand cmd, CancellationToken cancellationToken)
    {
        Timer timer = new(cmd.Hours, cmd.Minutes, cmd.Seconds);
        var reminder = Reminder.Define(timer, cmd.Address);

        await service.AppendEventsAsync<Reminder, ReminderId>(reminder, cancellationToken);

        DelayedEvent.ReminderElapsed @event = new(reminder.Id, reminder.Address);
        await service.SchedulePublishAsync(@event, reminder.ScheduledTime, cancellationToken);

        return new(reminder.Id, reminder.Status, reminder.TimeLeft.TotalSeconds);
    }
}