using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlSugar.Shared;

namespace MS.Client.IService
{
    public interface IUserService :IBaseService<UserTBDto>
    {
        Task<ApiResponse> GetMenusByUserIdAsync(int id);
        Task<ApiResponse> GetRolesByUserIdAsync(int id);
        Task<ApiResponse> BatchUpdateUserRoleInfoAsync(UserBatchModel param);
    }
}
