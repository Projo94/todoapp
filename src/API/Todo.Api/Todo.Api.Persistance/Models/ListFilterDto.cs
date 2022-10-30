using System;

namespace Todo.Api.Persistance.Models
{
    public class ListFilterDto
    {
        public int Page { get; set; }

        public int PageSize { get; set; }

        public string UserId { get; set; }

        public string Title { get; set; }

        public DateTimeOffset? Date { get; set; }
    }
}