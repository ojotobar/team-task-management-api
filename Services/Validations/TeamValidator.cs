using FluentValidation;
using Shared.DTO;

namespace Services.Validations
{
    public class TeamValidator : AbstractValidator<CreateTeamDto>
    {
        public TeamValidator()
        {
            RuleFor(x => x.TeamName)
                .NotEmpty().WithMessage("Team Name field is required.");
        }
    }
}
