using Todo.Api.Application.Models.Filters;
using Entities = Todo.Api.Domain.Entities;

namespace Todo.Api.Application.Contracts.Persistence
{
    public interface ITaskRepository : IAsyncRepository<Entities.Task>
    {
        Task<List<Entities.Task>> GetItemsAsync(TaskFilterDto filter);
    }
}
