using FluentValidation;
using GameClubAndEvent.Api.Contracts.Request;

namespace GameClubAndEvent.Api.Features.CreateClub;

public class CreateClubRequestValidator : AbstractValidator<CreateClubRequest>
{
    public CreateClubRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotNull().WithMessage("Club's name required.")
            .NotEmpty().WithMessage("Club's name required.")
            .MaximumLength(100).WithMessage("Club's name cannot exceed 100 characters");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Club's description cannot exceed 500 characters");
    }
}
