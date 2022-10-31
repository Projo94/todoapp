using MediatR;

namespace Todo.Api.Application.Features.Task.Commands.CreateTask
{
    public class CreateTaskCommand : IRequest<CreateTaskCommandResponse>
    {
        public int TaskListId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTimeOffset Deadline { get; set; }

        public bool IsDone { get; set; }
    }
}
