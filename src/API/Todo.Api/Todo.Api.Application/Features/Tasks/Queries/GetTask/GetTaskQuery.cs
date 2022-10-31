using MediatR;
using Todo.Api.Application.Filtering;

namespace Todo.Api.Application.Features.Task.Queries.GetTask
{
    public class GetTaskQuery : IRequest<List<TaskVm>>
    {
        public TaskFilter filter { get; set; }
    }
}