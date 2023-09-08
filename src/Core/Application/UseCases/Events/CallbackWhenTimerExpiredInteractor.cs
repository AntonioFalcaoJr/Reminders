using Application.Abstractions;
using Application.Services;
using Domain.Aggregates;
using Domain.Aggregates.Events;
using Serilog;

namespace Application.UseCases.Events;

public interface ICallbackWhenReminderElapsedInteractor : IInteractor<DelayedEvent.ReminderElapsed>;

public class CallbackWhenReminderElapsedInteractor(IApplicationService service, IHttpClientFactory httpClientFactory)
    : ICallbackWhenReminderElapsedInteractor
{
    public async Task InteractAsync(DelayedEvent.ReminderElapsed @event, CancellationToken cancellationToken)
    {
        var reminder = await service.LoadAggregateAsync<Reminder, ReminderId>((ReminderId)@event.ReminderId, cancellationToken);

        try
        {
            var client = httpClientFactory.CreateClient();
            var endpoint = $"{@event.Address}{@event.ReminderId}";
            await client.PostAsync(endpoint, new StringContent(string.Empty), cancellationToken);
            
            reminder.MarkAsCompleted();
        }
        catch (Exception e)
        {
            reminder.MarkAsFailed();
            Log.Error(e, "Failed to send callback to {@Address}", @event.Address);
        }

        await service.AppendEventsAsync<Reminder, ReminderId>(reminder, cancellationToken);
    }
}