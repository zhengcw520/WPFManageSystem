using MySqlSugar.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Client.Service
{
    public class OpLogService : IOpLogService
    {
        private readonly string serviceName = "Op-Log";
        private readonly string ApiName = "github";

        public async Task<FurApiResponse<SqlSugarPagedList<OpLogDto>>> GetPageListAsync(FindParameter parameter)
        {
            //https://localhost:44342/api/role/page-list?PageIndex=1&PageSize=10
            string exp = $"api/{serviceName}/page-list";
            return await exp.SetClient(ApiName)
                .SetQueries(parameter)
                .GetAsAsync<FurApiResponse<SqlSugarPagedList<OpLogDto>>>();
        }
    }
}
