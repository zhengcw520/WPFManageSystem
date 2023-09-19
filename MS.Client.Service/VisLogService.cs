using MySqlSugar.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Client.Service
{
    public class VisLogService : IVisLogService
    {
        private readonly string serviceName = "Vis-Log";
        private readonly string ApiName = "github";

        public async Task<FurApiResponse<SqlSugarPagedList<VisLogDto>>> GetPageListAsync(FindParameter parameter)
        {
            //https://localhost:44342/api/role/page-list?PageIndex=1&PageSize=10
            string exp = $"api/{serviceName}/page-list";
            var model = await exp.SetClient(ApiName)
                .SetQueries(parameter)
                .GetAsAsync<FurApiResponse<SqlSugarPagedList<VisLogDto>>>();
            return model;
        }
    }
}
