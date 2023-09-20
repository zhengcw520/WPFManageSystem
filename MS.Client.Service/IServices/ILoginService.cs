namespace MS.Client.Service
{
    public interface ILoginService
    {
        Task<FurApiResponse<LoginDto>> Login(LoginInput user);

        Task<FurApiResponse<bool>> Resgiter(UserDto user);
    }
}
