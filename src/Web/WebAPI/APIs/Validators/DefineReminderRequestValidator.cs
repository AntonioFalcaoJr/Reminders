using FluentValidation;
using WebAPI.APIs.Requests;

namespace WebAPI.APIs.Validators;

public class DefineReminderRequestValidator : AbstractValidator<DefineReminderRequest>
{
    public DefineReminderRequestValidator()
    {
        RuleFor(x => x.Hours)
            .InclusiveBetween(ushort.MinValue, ushort.MaxValue);

        RuleFor(x => x.Minutes)
            .InclusiveBetween(ushort.MinValue, ushort.MaxValue);

        RuleFor(x => x.Seconds)
            .InclusiveBetween(ushort.MinValue, ushort.MaxValue);

        When(request => request is { Hours: 0, Minutes: 0, Seconds: 0 }, () =>
        {
            RuleFor(x => x)
                .Must(_ => false)
                .WithMessage("At least one of the following properties must be greater than zero: Hours, Minutes, Seconds");
        });

        RuleFor(x => x.Address)
            .NotNull()
            .NotEmpty()
            .Must(address => Uri.TryCreate(address, UriKind.Absolute, out _))
            .WithMessage("Address must be a valid URI");
    }
}