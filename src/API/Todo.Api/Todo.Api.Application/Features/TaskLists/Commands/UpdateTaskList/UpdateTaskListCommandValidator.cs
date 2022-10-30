using FluentValidation;
using Todo.Api.Application.Helpers;

namespace Todo.Api.Application.Features.TaskLists.Commands.UpdateTaskList
{
    public sealed class UpdateTaskListCommandValidator : AbstractValidator<UpdateTaskListCommand>
    {
        public UpdateTaskListCommandValidator()
        {
            RuleFor(x => x).Must(p => p.TimeZone.IsValidTimezone()).WithMessage("Timezone is not valid!");

            RuleFor(x => x.DailyList).NotEmpty();
        }
    }
}
