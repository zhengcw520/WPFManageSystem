﻿namespace MS.Client.Service
{
    public class RoleService : BaseService<RoleDto>, IRoleService
    {
        private readonly string serviceName = "Role";
        private readonly string ApiName = "github";

        public RoleService() : base("Role", "github")
        {
        }

        public async Task<FurApiResponse> GetMenusByRoleIdAsync(int id)
        {
            return await $"api/{serviceName}/menus-by-role-id/{id}".SetClient(ApiName).GetAsAsync<FurApiResponse>();
        }   
        
        public async Task<FurApiResponse> GetUsersByRoleIdAsync(int id)
        {
            return await $"api/{serviceName}/users-by-role-id/{id}".SetClient(ApiName).GetAsAsync<FurApiResponse>();
        }

        /// <summary>
        /// 菜单编辑画面保存
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<FurApiResponse> BatchUpdateRoleInfoAsync(RoleBatchModel model)
        {
            return await $"api/{serviceName}/batch-update".SetBody(model).SetClient(ApiName).PostAsAsync<FurApiResponse>();
        }

        /// <summary>
        /// 批量插入角色菜单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<FurApiResponse> BatchInsertAsync(RoleBatchModel model)
        {
            return await $"api/{serviceName}/batch-insert".SetBody(model).SetClient(ApiName).PostAsAsync<FurApiResponse>();
        }

        ///// <summary>
        ///// 批量删除角色菜单
        ///// </summary>
        ///// <param name="model"></param>
        ///// <returns></returns>
        //public async Task<FurApiResponse> BatchDeleteAsync(RoleBatchModel model)
        //{
        //    BaseRequest request = new BaseRequest();
        //    request.Method = Method.Post;
        //    request.Route = $"api/{serviceName}/BatchDelete";
        //    request.Parameter = model;
        //    return await client.ExecuteAsync(request);
        //}

        ///// <summary>
        ///// 批量更新数据
        ///// </summary>
        ///// <param name="model"></param>
        ///// <returns></returns>
        //public async Task<FurApiResponse> BatchUpdateAsync(RoleBatchModel model)
        //{
        //    BaseRequest request = new BaseRequest();
        //    request.Method = Method.Post;
        //    request.Route = $"api/{serviceName}/BatchUpdate";
        //    request.Parameter = model;
        //    return await client.ExecuteAsync(request);
        //}
    }
}
