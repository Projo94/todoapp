using MediatR;

namespace Todo.Api.Application.Features.TaskLists.Commands.CreateTaskList
{
    public class CreateTaskListCommand : IRequest<CreateTaskListCommandResponse>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string TimeZone { get; set; }

        public DateTimeOffset DailyList { get; set; }

        public string CreatedByUserId { get; set; }
    }

    public class CreateTaskListDto : IRequest<CreateTaskListCommandResponse>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string TimeZone { get; set; }

        public DateTimeOffset DailyList { get; set; }
    }
}