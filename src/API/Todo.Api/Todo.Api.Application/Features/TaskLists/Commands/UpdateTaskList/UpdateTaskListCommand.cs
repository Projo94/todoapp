using MediatR;

namespace Todo.Api.Application.Features.TaskLists.Commands.UpdateTaskList
{
    public class UpdateTaskListCommand : IRequest
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string TimeZone { get; set; }

        public DateTimeOffset DailyList { get; set; }

        public string CreatedByUserId { get; set; }
    }

    public class UpdateTaskListDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string TimeZone { get; set; }

        public DateTimeOffset DailyList { get; set; }
    }
}