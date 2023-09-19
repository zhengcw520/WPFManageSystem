using MySqlSugar.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Client.Service
{
    public interface IVisLogService
    {
        Task<FurApiResponse<SqlSugarPagedList<VisLogDto>>> GetPageListAsync(FindParameter parameter);
    }
}
