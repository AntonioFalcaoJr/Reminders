using Application.UseCases.Commands;
using Application.UseCases.Queries;
using Microsoft.AspNetCore.Http.HttpResults;
using WebAPI.APIs.Requests;
using WebAPI.APIs.Validators;
using static Microsoft.AspNetCore.Http.TypedResults;

namespace WebAPI.APIs;

public static class RemindersApi
{
    private const string BaseUrl = "/api/v1/reminders/";

    public static void MapRemindersApi(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup(BaseUrl);

        group.MapPost("/", async (IDefineReminderInteractor interactor, DefineReminderRequest request, CancellationToken cancellationToken) =>
        {
            var result = await new DefineReminderRequestValidator().ValidateAsync(request, cancellationToken);
            return result.IsValid ? await DefineReminderAsync() : ValidationProblem(result.ToDictionary());

            async Task<Results<Ok<DefineReminderResponse>, ValidationProblem>> DefineReminderAsync()
            {
                var response = await interactor.InteractAsync(request, cancellationToken);
                return Ok(response);
            }
        });

        group.MapMethods("/{reminderId}/", new[] { "GET", "POST" }, async (IGetScheduledTimeInteractor interactor, Guid reminderId, CancellationToken cancellationToken) =>
        {
            var query = new GetScheduledTimeQuery(reminderId);
            var result = await new GetScheduledTimeQueryValidator().ValidateAsync(query, cancellationToken);
            return result.IsValid ? await GetScheduledTimeAsync() : ValidationProblem(result.ToDictionary());

            async Task<Results<Ok<GetScheduledTimeResponse>, ValidationProblem>> GetScheduledTimeAsync()
            {
                var response = await interactor.InteractAsync(query, cancellationToken);
                return Ok(response);
            }
        });
    }
}