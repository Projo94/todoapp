namespace Todo.Api.Application.Contracts.Persistence
{
    public interface IAsyncRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);

        Task<IReadOnlyList<T>> ListAllAsync();

        Task<T> AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);

        Task SaveChangesAsync();

        Task<IReadOnlyList<T>> GetPagedReponseAsync(int page, int size);

        Task AddRange(List<T> entityList);

        Task<bool> Exists(object id);
    }
}