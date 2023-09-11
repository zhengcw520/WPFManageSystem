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
    public class LoginService : ILoginService
    {
        private readonly HttpRestClient client;
        private readonly string serviceName = "Login";

        public LoginService(HttpRestClient client)
        {
            this.client = client;
        }

        public async Task<ApiResponse> Login(UserTBDto user)
        {
            BaseRequest request = new BaseRequest();
            request.Method = Method.Get;
            request.Route = $"api/{serviceName}/Login?Account={user.Account}&Password={user.Password}";
            return await client.ExecuteAsync(request);
        }

        public async Task<ApiResponse> Resgiter(UserTBDto user)
        {
            BaseRequest request = new BaseRequest();
            request.Method = Method.Post;
            request.Route = $"api/{serviceName}/Resgiter";
            request.Parameter = user;
            return await client.ExecuteAsync(request);
        }
    }
}
