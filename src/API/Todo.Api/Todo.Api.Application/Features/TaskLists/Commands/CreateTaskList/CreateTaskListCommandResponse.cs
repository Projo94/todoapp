using Todo.Api.Application.Features.TaskList.Queries.GetTaskList;
using Todo.Api.Application.Responses;

namespace Todo.Api.Application.Features.TaskLists.Commands.CreateTaskList
{
    public class CreateTaskListCommandResponse : BaseResponse
    {
        public CreateTaskListCommandResponse() : base()
        {

        }
        public TaskListDto TaskList { get; set; }
    }
}