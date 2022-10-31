using Todo.Api.Application.Models.Filters;
using Todo.Api.Domain.Entities;

namespace Todo.Api.Application.Contracts.Persistence
{
    public interface ITaskListRepository : IAsyncRepository<TaskList>
    {
        Task<List<TaskList>> GetItemsAsync(ListFilterDto filter);

        Task<string> GetTimeZoneAsync(int Id);
    }
}