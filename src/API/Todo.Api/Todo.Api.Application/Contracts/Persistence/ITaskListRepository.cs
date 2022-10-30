using Todo.Api.Domain.Entities;

namespace Todo.Api.Application.Contracts.Persistence
{
    public interface ITaskListRepository : IAsyncRepository<TaskList>
    {
    }
}