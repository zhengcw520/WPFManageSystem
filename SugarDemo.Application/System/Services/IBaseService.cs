namespace MySqlSugar.IService
{
    public interface IBaseService<TEntity> where TEntity : class,new()
    {
        Task<SqlSugarPagedList<TEntity>> GetPageListAsync(FindParameter FindParameter);
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity> GetSingleAsync(int id);
        Task AddOrUpdateAsync(TEntity model);
        Task DeleteAsync(int id);
    }
}
