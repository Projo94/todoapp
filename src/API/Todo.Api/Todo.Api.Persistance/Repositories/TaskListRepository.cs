using AutoMapper;
using Todo.Api.Application.Contracts.Persistence;
using Todo.Api.Domain.Entities;

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
    }
}