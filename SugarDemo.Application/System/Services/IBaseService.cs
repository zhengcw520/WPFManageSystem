namespace SugarDemo.Application
{
    public interface IBaseService<TEntity> where TEntity : class,new()
    {
        Task<SugarPagedList<TEntity>> GetPageListAsync(FindParameter FindParameter);
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity> GetSingleAsync(int id);
        Task AddOrUpdateAsync(TEntity model);
        Task DeleteAsync(int id);
    }
}
