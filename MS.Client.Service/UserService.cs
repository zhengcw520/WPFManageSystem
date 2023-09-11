using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MS.Client.IService;
using MS.Client.RestSharp;
using MySqlSugar.Shared;

namespace MS.Client.Service
{
    public class UserService : BaseService<UserTBDto>, IUserService
    {
        private readonly HttpRestClient client;
        private readonly string serviceName = "User";

        public UserService(HttpRestClient client) : base(client, "User")
        {
            this.client = client;
        }

        public async Task<ApiResponse> GetMenusByUserIdAsync(int id)
        {
            BaseRequest request = new BaseRequest();
            request.Method = Method.Get;
            request.Route = $"api/{serviceName}/GetMenusByUserId?id={id}";
            return await client.ExecuteAsync(request);
        }

        public async Task<ApiResponse> GetRolesByUserIdAsync(int id)
        {
            BaseRequest request = new BaseRequest();
            request.Method = Method.Get;
            request.Route = $"api/{serviceName}/GetRolesByUserId?id={id}";
            return await client.ExecuteAsync(request);
        }

        public async Task<ApiResponse> BatchUpdateUserRoleInfoAsync(UserBatchModel model)
        {
            BaseRequest request = new BaseRequest();
            request.Method = Method.Post;
            request.Route = $"api/{serviceName}/BatchUpdateUserRoleInfo";
            request.Parameter = model;
            return await client.ExecuteAsync(request);
        }
    }
}
