namespace MS.Client.Service
{
    public interface IBaseService<TEntity> where TEntity : class
    {
        Task<FurApiResponse> AddOrUpdateAsync(TEntity entity);

        Task<FurApiResponse> DeleteAsync(int id);

        Task<FurApiResponse> GetFirstOfDefaultAsync(int id);

        Task<FurApiResponse> GetPageListAsync(FindParameter parameter);

        Task<FurApiResponse> GetAllAsync();
    }
}
