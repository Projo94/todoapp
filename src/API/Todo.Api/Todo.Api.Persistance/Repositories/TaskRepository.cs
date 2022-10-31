using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Todo.Api.Application.Contracts.Persistence;
using Todo.Api.Application.Models.Filters;
using Entities = Todo.Api.Domain.Entities;

namespace Todo.Api.Persistance.Repositories
{
    public class TaskRepository : BaseRepository<Entities.Task>, ITaskRepository
    {
        private readonly TodoApiDbContext _context;

        private readonly IMapper _mapper;

        public TaskRepository(TodoApiDbContext context, IMapper mapper) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper;
        }

        public async Task<List<Entities.Task>> GetItemsAsync(Application.Models.Filters.TaskFilterDto filterDto)
        {
            //var filter = _mapper.Map<ListFilterDto>(filterDto);

            var filter = new TaskFilterDto
            {
                TaskListId = filterDto.TaskListId,
                Deadline = filterDto.Deadline,
                IsDone = filterDto.IsDone
            };

            var query = _context.Tasks
                .AsNoTracking()
                .AsQueryable(); ;

            query = ApplyFilter(query, filter);


            return await query.ToListAsync();
        }

        private IQueryable<Entities.Task> ApplyFilter(IQueryable<Entities.Task> source, TaskFilterDto filter)
        {
            source = source.Where(a => a.TaskListId == filter.TaskListId);

            if (filter.IsDone.HasValue)
            {
                source = source.Where(a => a.IsDone == filter.IsDone);
            }

            if (filter.Deadline.HasValue)
            {
                var date = filter.Deadline.Value;
                source = source.Where(a => a.Deadline.Date == date.Date);
            }

            return source;
        }
    }
}