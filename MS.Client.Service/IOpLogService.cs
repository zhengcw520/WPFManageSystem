using MySqlSugar.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Client.Service
{
    public interface IOpLogService
    {
        Task<FurApiResponse<SqlSugarPagedList<OpLogDto>>> GetPageListAsync(FindParameter parameter);
    }
}
