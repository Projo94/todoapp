using MediatR;

namespace Todo.Api.Application.Features.Task.Commands.UpdateTask
{
    public class UpdateTaskCommand : IRequest
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTimeOffset Deadline { get; set; }

        public bool IsDone { get; set; }

    }
}