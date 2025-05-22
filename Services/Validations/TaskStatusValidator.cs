using FluentValidation;
using Shared.DTO;

namespace Services.Validations
{
    public class TaskStatusValidator : AbstractValidator<TaskStatusDto>
    {
        public TaskStatusValidator()
        {
            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Please enter a valid task status");
        }
    }
}
