namespace MySqlSugar.IService
{
    public interface IBaseService<T>
    {
        Task<List<T>> GetPageListAsync(FindParameter FindParameter);
        Task<List<T>> GetAllAsync();
        Task<T> GetSingleAsync(int id);
        Task<bool> AddOrUpdateAsync(T model);
        Task<bool> DeleteAsync(int id);
    }
}
