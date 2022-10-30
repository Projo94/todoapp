namespace Todo.Api.Application.Models.Filters
{
    public class ListFilterDto
    {
        public int Page { get; set; }

        public int PageSize { get; set; }

        public string UserId { get; set; }

        public string Title { get; set; }

        public DateTime? Date { get; set; }
    }
}
