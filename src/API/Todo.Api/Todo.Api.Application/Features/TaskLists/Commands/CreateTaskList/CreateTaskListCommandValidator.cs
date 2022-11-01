using FluentValidation;
using Todo.Api.Application.Helpers;

namespace Todo.Api.Application.Features.TaskLists.Commands.CreateTaskList
{
    public sealed class CreateTaskListCommandValidator : AbstractValidator<CreateTaskListCommand>
    {
        public CreateTaskListCommandValidator()
        {
            RuleFor(x => x.TimeZone).Must(ValidTimezone)
                .WithMessage("Timezone is not valid!");

            RuleFor(x => x.DailyList).NotNull().NotEmpty();

            RuleFor(x => x).Must(x =>
                x.Title.Length > 0 &&
                x.Title.Length <= 50)
                .WithMessage("Length of Title field is required and could be 50 max characters ")
                .NotNull();

            RuleFor(x => x).Must(x =>
                x.Description.Length > 0 &&
                x.Description.Length <= 100)
                .WithMessage("Length of Description field is required and could be 100 max characters ")
                .NotNull();
        }

        private bool ValidTimezone(string timezone)
        {
            return timezone.IsValidTimezone();
        }
    }
}