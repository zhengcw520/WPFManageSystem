namespace SugarDemo.Application
{
    public interface ILoginService
    {
        Task<bool> LoginAsync(string Account, string Password);
        Task<bool> RegisterAsync(UserTBDto UserTBDto);
    }
}
