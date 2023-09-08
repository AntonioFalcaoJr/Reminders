using Application.UseCases.Queries;
using Domain.Aggregates;
using FluentValidation;

namespace WebAPI.APIs.Validators;

public class GetScheduledTimeQueryValidator : AbstractValidator<GetScheduledTimeQuery>
{
    public GetScheduledTimeQueryValidator()
    {
        RuleFor(x => x.ReminderId)
            .NotNull()
            .NotEmpty()
            .Must(id => Guid.TryParse(id, out _)).WithMessage("ReminderId must be a valid GUID")
            .Must(id => id != ReminderId.Undefined);
    }
}