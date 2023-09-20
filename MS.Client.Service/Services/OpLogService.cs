namespace MS.Client.Service
{
    public class OpLogService : IOpLogService
    {
        private readonly string serviceName = "Op-Log";
        private readonly string ApiName = "github";

        public async Task<FurApiResponse<SugarPagedList<OpLogDto>>> GetPageListAsync(FindParameter parameter)
        {
            //https://localhost:44342/api/role/page-list?PageIndex=1&PageSize=10
            string exp = $"api/{serviceName}/page-list";
            return await exp.SetClient(ApiName)
                .SetQueries(parameter)
                .GetAsAsync<FurApiResponse<SugarPagedList<OpLogDto>>>();
        }
    }
}
