namespace Inspector.Application.Contracts.Database.Repositories
{
    public interface IBaseRepository<T>
    {
        Task<T> CreateAsync(T baseEntity);
        Task<T> UpdateAsync(T baseEntity);
        Task DeleteAsync(int id);
        Task<T> GetAsync(int Id);
        Task SaveChangesAsync();
        Task<List<T>> GetAll();
    }
}
