namespace SugarDemo.Application
{
    public interface ILoginService
    {
        Task<LoginInfoModel> LoginAsync(string Account, string Password);
        Task RegisterAsync(UserTBDto UserTBDto);
    }
}
