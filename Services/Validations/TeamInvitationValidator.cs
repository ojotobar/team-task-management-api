using FluentValidation;
using Shared.DTO;

namespace Services.Validations
{
    internal class TeamInvitationValidator : AbstractValidator<TeamInvitaionDto>
    {
        public TeamInvitationValidator()
        {
            RuleFor(x => x.UserIds)
                .Must(CustomValidations.BeValidUserIds).WithMessage("One or more user ids are invalid.");
        }
    }
}
