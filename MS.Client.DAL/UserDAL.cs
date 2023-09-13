namespace MS.Client.DAL
{
    public class UserDAL : BaseDAL<UserTBDto>, IUserDAL
    {
        private readonly string DALName = "User";
        private readonly string ApiName = "github";

        public UserDAL() : base("User","github")
        {
        }

        public async Task<FurApiResponse> GetMenusByUserIdAsync(int id)
        {
            //https://localhost:44342/api/user/menus-by-user-id/1
            return await $"api/{DALName}/menus-by-user-id/{id}".SetClient(ApiName).GetAsAsync<FurApiResponse>();
        }

        public async Task<FurApiResponse> GetRolesByUserIdAsync(int id)
        {
            //https://localhost:44342/api/user/roles-by-user-id/1
            return await $"api/{DALName}/roles-by-user-id/{id}".SetClient(ApiName).GetAsAsync<FurApiResponse>();
        }
    }
}
