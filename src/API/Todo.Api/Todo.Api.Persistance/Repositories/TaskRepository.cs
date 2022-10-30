using AutoMapper;
using Todo.Api.Application.Contracts.Persistence;
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
    }
}