using FluentValidation;
using GameClubAndEvent.Api.Contracts.Request;

namespace GameClubAndEvent.Api.Features.CreateEvent;

public class CreateEventValidator : AbstractValidator<CreateEventRequest>
{
    public CreateEventValidator()
    {
        RuleFor(x => x.Title)
            .NotNull().WithMessage("Event's title required.")
            .NotEmpty().WithMessage("Event's title required.")
            .MaximumLength(100).WithMessage("Event's title cannot exceed 100 characters");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Event's description cannot exceed 500 characters");

        var now = DateTime.UtcNow;
        RuleFor(x => x.ScheduledTime)
            .NotNull().WithMessage("Event's scheduled date/time required.")
            .Must((schedule) => schedule.ToUniversalTime() >= now).WithMessage("Event's scheduled date/time must not be in the pass.");
    }
}
