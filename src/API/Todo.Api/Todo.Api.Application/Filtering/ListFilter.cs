namespace Todo.Api.Application.Filtering
{
    public class ListFilter
    {
        public int Page { get; set; }

        public int PageSize { get; set; } = 10;

        public string UserId { get; set; }

        public string Title { get; set; }

        public DateTime? Date { get; set; }
    }



    public class ListFilterDto
    {
        public int Page { get; set; }

        public string Title { get; set; }

        public DateTime? Date { get; set; }
    }



}
