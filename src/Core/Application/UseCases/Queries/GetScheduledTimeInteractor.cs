using Application.Abstractions;
using Application.Services;
using Domain.Aggregates;

namespace Application.UseCases.Queries;

public record GetScheduledTimeQuery(ReminderId ReminderId);

public record GetScheduledTimeResponse(string Id, string Status, double TimeLeft);

public interface IGetScheduledTimeInteractor : IInteractor<GetScheduledTimeQuery, GetScheduledTimeResponse>;

public class GetScheduledTimeInteractor(IApplicationService service) : IGetScheduledTimeInteractor
{
    public async Task<GetScheduledTimeResponse> InteractAsync(GetScheduledTimeQuery query, CancellationToken cancellationToken)
    {
        var reminder = await service.LoadAggregateAsync<Reminder, ReminderId>(query.ReminderId, cancellationToken);
        return new(reminder.Id, reminder.Status, reminder.TimeLeft.TotalSeconds);
    }
}