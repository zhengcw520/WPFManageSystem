namespace MS.Client.Service
{
    public class UserService : BaseService<UserDto>, IUserService
    {
        private readonly string serviceName = "User";
        private readonly string ApiName = "github";

        public UserService() : base("User", "github")
        {
        }

        public async Task<FurApiResponse<List<MenuDto>>> GetMenusByUserIdAsync(int id)
        {
            //https://localhost:44342/api/user/menus-by-user-id/1
            return await $"api/{serviceName}/menus-by-user-id/{id}"
                .SetClient(ApiName)
                .GetAsAsync<FurApiResponse<List<MenuDto>>>();
        }

        public async Task<FurApiResponse<List<RoleDto>>> GetRolesByUserIdAsync(int id)
        {
            //https://localhost:44342/api/user/roles-by-user-id/1
            return await $"api/{serviceName}/roles-by-user-id/{id}".SetClient(ApiName).GetAsAsync<FurApiResponse<List<RoleDto>>>();
        }

        public async Task<FurApiResponse<bool>> BatchUserRolesAsync(UserBatchModel model)
        {
            //https://localhost:44342/api/user/roles-by-user-id/1
            return await $"api/{serviceName}/roles-by-user-id".SetClient(ApiName).SetBody(model).PostAsAsync<FurApiResponse<bool>>();
        }
    }
}
