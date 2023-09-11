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
    public class RoleService : BaseService<RoleDto>, IRoleService
    {
        private readonly HttpRestClient client;
        private readonly string serviceName = "Role";

        public RoleService(HttpRestClient client) : base(client, "Role")
        {
            this.client = client;
        }

        public async Task<ApiResponse> GetMenusByRoleIdAsync(int roleId)
        {
            BaseRequest request = new BaseRequest();
            request.Method = Method.Get;
            request.Route = $"api/{serviceName}/GetMenusByRoleId?id={roleId}";
            return await client.ExecuteAsync(request);
        }   
        
        public async Task<ApiResponse> GetUsersByRoleIdAsync(int roleId)
        {
            BaseRequest request = new BaseRequest();
            request.Method = Method.Get;
            request.Route = $"api/{serviceName}/GetUsersByRoleId?id={roleId}";
            return await client.ExecuteAsync(request);
        }

        /// <summary>
        /// 菜单编辑画面保存
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ApiResponse> BatchUpdateRoleInfoAsync(RoleBatchModel model)
        {
            BaseRequest request = new BaseRequest();
            request.Method = Method.Post;
            request.Route = $"api/{serviceName}/BatchUpdateRoleInfo";
            request.Parameter = model;
            return await client.ExecuteAsync(request);
        }

        /// <summary>
        /// 批量插入角色菜单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ApiResponse> BatchInsertAsync(RoleBatchModel model)
        {
            BaseRequest request = new BaseRequest();
            request.Method = Method.Post;
            request.Route = $"api/{serviceName}/BatchInsert";
            request.Parameter = model;
            return await client.ExecuteAsync(request);
        }

        /// <summary>
        /// 批量删除角色菜单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ApiResponse> BatchDeleteAsync(RoleBatchModel model)
        {
            BaseRequest request = new BaseRequest();
            request.Method = Method.Post;
            request.Route = $"api/{serviceName}/BatchDelete";
            request.Parameter = model;
            return await client.ExecuteAsync(request);
        }

        /// <summary>
        /// 批量更新数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ApiResponse> BatchUpdateAsync(RoleBatchModel model)
        {
            BaseRequest request = new BaseRequest();
            request.Method = Method.Post;
            request.Route = $"api/{serviceName}/BatchUpdate";
            request.Parameter = model;
            return await client.ExecuteAsync(request);
        }
    }
}
