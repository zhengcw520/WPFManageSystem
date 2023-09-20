namespace MS.Client.Service
{
    public interface IBaseService<TEntity> where TEntity : class, new()
    {
        Task<FurApiResponse> AddOrUpdateAsync(TEntity entity);

        Task<FurApiResponse> DeleteAsync(int id);

        Task<FurApiResponse<TEntity>> GetFirstOfDefaultAsync(int id);

        Task<FurApiResponse<SugarPagedList<TEntity>>> GetPageListAsync(FindParameter parameter);

        Task<FurApiResponse<List<TEntity>>> GetAllAsync();
    }
}
