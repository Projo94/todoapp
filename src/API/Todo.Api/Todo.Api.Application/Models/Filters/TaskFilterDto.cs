namespace Todo.Api.Application.Models.Filters
{
    public class TaskFilterDto
    {
        public int TaskListId { get; set; }

        public DateTimeOffset? Deadline { get; set; }

        public bool? IsDone { get; set; }
    }
}
