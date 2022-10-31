using System.Text.Json.Serialization;

namespace Todo.Api.Application.Features.Task.Queries.GetTask
{
    public class TaskVm
    {
        public int Id { get; set; }

        [JsonIgnore]
        public int TaskListId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        [JsonIgnore]
        public DateTimeOffset Deadline { get; set; }

        public string DeadlineVm { get; set; }

        public bool IsDone { get; set; }
    }
}