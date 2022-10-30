using Entities = Todo.Api.Domain.Entities;

namespace Todo.Api.Application.Contracts.Persistence
{
    public interface ITaskRepository : IAsyncRepository<Entities.Task>
    {
    }
}
