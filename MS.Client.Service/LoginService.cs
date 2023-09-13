namespace MS.Client.Service
{
    public class LoginService : ILoginService
    {
        private readonly string serviceName = "login";
        private readonly string ApiName = "github";

        public LoginService()
        {
        }

        public async Task<FurApiResponse> Login(UserTBDto user)
        {
            var model = await $"api/{serviceName}/login/{user.Account}/{user.Password}".SetClient(ApiName).GetAsAsync<FurApiResponse>();
            return model;
        }

        public async Task<FurApiResponse> Resgiter(UserTBDto user)
        {
            //https://localhost:44342/api/login/register
            return await $"api/{serviceName}/register".SetClient(ApiName).SetBody(user).PostAsAsync<FurApiResponse>();
        }
    }
}
