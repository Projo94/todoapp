using MediatR;

namespace Todo.Api.Application.Features.Task.Commands.DeleteTask
{
    public class DeleteTaskCommand : IRequest
    {
        public int Id { get; set; }
    }
}
