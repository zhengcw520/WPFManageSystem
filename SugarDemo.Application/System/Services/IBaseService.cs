namespace MySqlSugar.IService
{
    public interface IBaseService<T>
    {
        Task<List<T>> GetPageListAsync(FindParameter FindParameter);
        Task<List<T>> GetAllAsync();
        Task<T> GetSingleAsync(int id);
        Task AddOrUpdateAsync(T model);
        Task DeleteAsync(int id);
    }
}
