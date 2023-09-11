using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlSugar.Shared;

namespace MS.Client.IService
{
    public interface IBaseService<TEntity> where TEntity : class
    {
        Task<ApiResponse<TEntity>> AddOrUpdateAsync(TEntity entity);

        Task<ApiResponse> DeleteAsync(int id);

        Task<ApiResponse<TEntity>> GetFirstOfDefaultAsync(int id);

        Task<ApiResponse<List<TEntity>>> GetPageListAsync(FindParameter parameter);

        Task<ApiResponse<List<TEntity>>> GetAllAsync();
    }
}
