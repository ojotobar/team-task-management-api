using FluentValidation;
using Shared.DTO;

namespace Services.Validations
{
    public class TaskValidator : AbstractValidator<TaskDtoBase>
    {
        public TaskValidator()
        {
            RuleFor(t => t.TaskTitle)
                .NotEmpty().WithMessage("The Task Title field is required");
            RuleFor(t => t.Description)
                .NotEmpty().WithMessage("{Property} field is required");
            RuleFor(t => t.AssignTo)
                .Must(CustomValidations.BeAValidUserId)
                .WithMessage("User Id is invalid.");
            RuleFor(t => t.DueOn)
                .Must(CustomValidations.BeAValidFutureDate)
                .WithMessage("Due Date must be in the future");
        }
    }
}