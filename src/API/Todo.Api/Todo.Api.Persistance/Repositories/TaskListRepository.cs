using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Todo.Api.Application.Contracts.Persistence;
using Todo.Api.Domain.Entities;
using Todo.Api.Persistance.Models;

namespace Todo.Api.Persistance.Repositories
{
    public class TaskListRepository : BaseRepository<TaskList>, ITaskListRepository
    {
        private readonly TodoApiDbContext _context;

        private readonly IMapper _mapper;

        public TaskListRepository(TodoApiDbContext context, IMapper mapper) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper;
        }

        public async Task<List<TaskList>> GetItemsAsync(Application.Models.Filters.ListFilterDto filterDto)
        {
            var filter = _mapper.Map<ListFilterDto>(filterDto);

            var query = _context.TaskLists
                .AsNoTracking()
                .AsQueryable(); ;

            query = ApplyFilter(query, filter);

            var page = filter.Page;
            var size = filter.PageSize;

            return await query.Skip((page - 1) * size).Take(size).ToListAsync();
        }

        private IQueryable<TaskList> ApplyFilter(IQueryable<TaskList> source, ListFilterDto filter)
        {
            source = source.Where(a => a.CreatedByUserId == filter.UserId);

            if (!string.IsNullOrEmpty(filter.Title))
            {
                source = source.Where(a => a.Title.ToUpper().Contains(filter.Title.ToUpper()));
            }

            if (filter.Date.HasValue)
            {
                var date = filter.Date.Value;
                source = source.Where(a => a.DailyList == date); //TODO change two dates on filter, FromDate and ToDate
            }

            return source;
        }
    }
}