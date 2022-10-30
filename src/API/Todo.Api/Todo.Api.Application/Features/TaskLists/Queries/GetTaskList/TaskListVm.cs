namespace Todo.Api.Application.Features.TaskList.Queries.GetTaskList
{
    public class TaskVm
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTimeOffset Deadline { get; set; }

        public bool IsDone { get; set; }
    }
}