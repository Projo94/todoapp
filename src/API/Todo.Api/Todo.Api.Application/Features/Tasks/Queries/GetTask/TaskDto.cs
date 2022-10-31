namespace Todo.Api.Application.Features.TaskList.Queries.GetTaskList
{
    public class TaskDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string DeadlineVm { get; set; }
    }
}
