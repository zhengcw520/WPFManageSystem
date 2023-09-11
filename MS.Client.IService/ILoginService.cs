using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlSugar.Shared;

namespace MS.Client.IService
{
    public interface ILoginService
    {
        Task<ApiResponse> Login(UserTBDto user);

        Task<ApiResponse> Resgiter(UserTBDto user);
    }
}
