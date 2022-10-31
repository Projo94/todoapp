namespace Todo.Api.Application.Filtering
{
    public class TaskFilter
    {
        public int TaskListId { get; set; }

        public DateTimeOffset? Deadline { get; set; }

        public bool? IsDone { get; set; }
    }
}
