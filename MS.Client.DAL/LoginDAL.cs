namespace MS.Client.DAL
{
    public class LoginDAL : ILoginDAL
    {
        private readonly string DALName = "Login";

        public LoginDAL()
        {
        }

        public async Task<FurApiResponse> Login(UserTBDto user)
        {
            return await $"api/{DALName}/Login/{user.Account}/{user.Password}".GetAsAsync<FurApiResponse>();
        }

        public async Task<FurApiResponse> Resgiter(UserTBDto user)
        {
            //https://localhost:44342/api/login/register
            return await $"api/{DALName}/register".SetBody(user).GetAsAsync<FurApiResponse>();
        }
    }
}
