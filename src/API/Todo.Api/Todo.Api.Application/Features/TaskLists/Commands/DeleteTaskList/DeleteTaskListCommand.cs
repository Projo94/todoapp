using MediatR;

namespace Todo.Api.Application.Features.TaskLists.Commands.DeleteTaskList
{
    public class DeleteTaskListCommand : IRequest
    {
        public int Id { get; set; }
    }
}
