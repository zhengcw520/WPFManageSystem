namespace MS.Client.Service
{
    public interface IOpLogService
    {
        Task<FurApiResponse<SugarPagedList<OpLogDto>>> GetPageListAsync(FindParameter parameter);
    }
}
