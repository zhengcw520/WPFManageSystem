namespace MS.Client.Service
{
    public class LoginService : ILoginService
    {
        private readonly string serviceName = "login";
        private readonly string ApiName = "github";

        public LoginService()
        {
        }

        public async Task<FurApiResponse<LoginInfoModel>> Login(LoginInput input)
        {
            //var model = await $"api/{serviceName}/login/{user.Account}/{user.Password}".SetClient(ApiName).GetAsAsync<FurApiResponse>();
            var model = await $"api/{serviceName}/login"
                .OnResponsing((client, res) => {
                // res 为 HttpResponseMessage 对象
            }).SetClient(ApiName).SetBody(input).PostAsAsync<FurApiResponse<LoginInfoModel>>();
            return model;
        }

        public async Task<FurApiResponse<bool>> Resgiter(UserTBDto user)
        {
            //https://localhost:44342/api/login/register
            return await $"api/{serviceName}/register".SetClient(ApiName).SetBody(user).PostAsAsync<FurApiResponse<bool>>();
        }
    }
}
