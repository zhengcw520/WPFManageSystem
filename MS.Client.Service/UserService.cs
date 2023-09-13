namespace MS.Client.Service
{
    public class UserService : BaseService<UserTBDto>, IUserService
    {
        private readonly string serviceName = "User";
        private readonly string ApiName = "github";

        public UserService() : base("User","github")
        {
        }

        public async Task<FurApiResponse> GetMenusByUserIdAsync(int id)
        {
            //https://localhost:44342/api/user/menus-by-user-id/1
            return await $"api/{serviceName}/menus-by-user-id/{id}".SetClient(ApiName).GetAsAsync<FurApiResponse>();
        }

        public async Task<FurApiResponse> GetRolesByUserIdAsync(int id)
        {
            //https://localhost:44342/api/user/roles-by-user-id/1
            return await $"api/{serviceName}/roles-by-user-id/{id}".SetClient(ApiName).GetAsAsync<FurApiResponse>();
        }

        public async Task<FurApiResponse> BatchUserRolesAsync(UserBatchModel model)
        {
            //https://localhost:44342/api/user/roles-by-user-id/1
            return await $"api/{serviceName}/roles-by-user-id".SetClient(ApiName).SetBody(model).PostAsAsync<FurApiResponse>();
        }
    }
}
