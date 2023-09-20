namespace MS.Client.Service
{
    public interface IVisLogService
    {
        Task<FurApiResponse<SugarPagedList<VisLogDto>>> GetPageListAsync(FindParameter parameter);
    }
}
