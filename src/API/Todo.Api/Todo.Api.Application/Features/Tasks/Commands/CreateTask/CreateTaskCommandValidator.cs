using FluentValidation;
using Todo.Api.Application.Features.Task.Commands.CreateTask;

namespace Todo.Api.Application.Features.Tasks.Commands.CreateTask
{
    public class CreateTaskCommandValidator : AbstractValidator<CreateTaskCommand>
    {
        public CreateTaskCommandValidator()
        {
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

            RuleFor(x => x.IsDone).NotEmpty();

            RuleFor(x => x.TaskListId).NotEmpty(); //TODO check does it exists

        }
    }
}
