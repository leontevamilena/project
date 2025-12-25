namespace LabWork9.Services
{
    public interface IService<T> where T : class
    {
        Task<List<T>> GetAsync();
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
    }
}
