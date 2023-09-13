namespace MS.Client.DAL
{
    public interface IBaseDAL<TEntity> where TEntity : class
    {
        Task<FurApiResponse> AddOrUpdateAsync(TEntity entity);

        Task<FurApiResponse> DeleteAsync(int id);

        Task<FurApiResponse> GetFirstOfDefaultAsync(int id);

        Task<FurApiResponse> GetPageListAsync(FindParameter parameter);

        Task<FurApiResponse> GetAllAsync();
    }
}
