using Application.UseCases.Commands;

namespace WebAPI.APIs.Requests;

public record DefineReminderRequest(int Hours, int Minutes, int Seconds, string Address)
{
    public static implicit operator DefineReminderCommand(DefineReminderRequest request)
        => new(new(request.Hours), new(request.Minutes), new(request.Seconds), new(request.Address));
}