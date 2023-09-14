namespace SugarDemo.Application
{
    public interface ILoginService
    {
        Task<LoginInfoModel> LoginAsync(LoginInput input);
        Task RegisterAsync(UserTBDto UserTBDto);
    }
}
