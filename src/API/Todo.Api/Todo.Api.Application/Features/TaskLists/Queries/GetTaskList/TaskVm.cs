using System.Text.Json.Serialization;
using Todo.Api.Application.Features.TaskList.Queries.GetTaskList;

namespace Todo.Api.Application.Features.Queries.GetTaskList
{
    public class TaskListVm
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        [JsonIgnore]
        public DateTimeOffset DailyList { get; set; }

        [JsonIgnore]
        public string Timezone { get; set; }

        public string DailyListVm { get; set; }
    }
}