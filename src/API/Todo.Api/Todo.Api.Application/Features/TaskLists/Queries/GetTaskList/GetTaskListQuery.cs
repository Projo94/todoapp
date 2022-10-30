using MediatR;
using Todo.Api.Application.Features.Queries.GetTaskList;
using Todo.Api.Application.Filtering;

namespace Todo.Api.Application.Features.TaskLists.Queries.GetTaskList
{
    public class GetTaskListQuery : IRequest<List<TaskListVm>>
    {
        public ListFilter filter { get; set; }
    }
}