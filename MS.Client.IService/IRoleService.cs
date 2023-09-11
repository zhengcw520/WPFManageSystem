using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlSugar.Shared;

namespace MS.Client.IService
{
    public interface IRoleService : IBaseService<RoleDto>
    {
        Task<ApiResponse> GetMenusByRoleIdAsync(int roleId);
        Task<ApiResponse> GetUsersByRoleIdAsync(int roleId);
        Task<ApiResponse> BatchInsertAsync(RoleBatchModel model);
        Task<ApiResponse> BatchDeleteAsync(RoleBatchModel model);
        Task<ApiResponse> BatchUpdateAsync(RoleBatchModel model);
        Task<ApiResponse> BatchUpdateRoleInfoAsync(RoleBatchModel model);
    }
}
