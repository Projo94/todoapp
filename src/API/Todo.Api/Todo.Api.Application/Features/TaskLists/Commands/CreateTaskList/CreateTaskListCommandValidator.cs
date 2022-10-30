using FluentValidation;
using Todo.Api.Application.Helpers;

namespace Todo.Api.Application.Features.TaskLists.Commands.CreateTaskList
{
    public sealed class CreateTaskListCommandValidator : AbstractValidator<CreateTaskListCommand>
    {
        public CreateTaskListCommandValidator()
        {
            RuleFor(x => x).Must(p => p.TimeZone.IsValidTimezone()).WithMessage("Timezone is not valid!");

            RuleFor(x => x.DailyList).NotEmpty();
        }
    }
}