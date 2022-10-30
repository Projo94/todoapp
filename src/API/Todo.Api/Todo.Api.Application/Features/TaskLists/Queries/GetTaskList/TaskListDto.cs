using System.Text.Json.Serialization;

namespace Todo.Api.Application.Features.TaskList.Queries.GetTaskList
{
    public class TaskListDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Timezone { get; set; }

        [JsonIgnore]
        public DateTimeOffset DailyList { get; set; }

        public string DailyListVm { get; set; }
    }
}
