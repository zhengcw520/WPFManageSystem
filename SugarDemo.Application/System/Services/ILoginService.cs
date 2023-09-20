namespace SugarDemo.Application
{
    public interface ILoginService
    {
        Task<LoginDto> LoginAsync(LoginInput input);
        Task RegisterAsync(UserDto UserDto);
    }
}
