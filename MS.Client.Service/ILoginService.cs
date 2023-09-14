namespace MS.Client.Service
{
    public interface ILoginService
    {
        Task<FurApiResponse<LoginInfoModel>> Login(LoginInput user);

        Task<FurApiResponse<bool>> Resgiter(UserTBDto user);
    }
}
