using FluentValidation;
using Todo.Api.Application.Features.Task.Commands.CreateTask;

namespace Todo.Api.Application.Features.Tasks.Commands.CreateTask
{
    public class CreateTaskCommandValidator : AbstractValidator<CreateTaskCommand>
    {
    }
}
