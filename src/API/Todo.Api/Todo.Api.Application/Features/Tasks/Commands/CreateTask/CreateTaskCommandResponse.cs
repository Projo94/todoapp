using Todo.Api.Application.Features.TaskList.Queries.GetTaskList;
using Todo.Api.Application.Responses;

namespace Todo.Api.Application.Features.Task.Commands.CreateTask
{
    public class CreateTaskCommandResponse : BaseResponse
    {
        public CreateTaskCommandResponse() : base()
        {

        }

        public TaskDto Task { get; set; }
    }
}