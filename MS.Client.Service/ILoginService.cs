namespace MS.Client.Service
{
    public interface ILoginService
    {
        Task<FurApiResponse> Login(UserTBDto user);

        Task<FurApiResponse> Resgiter(UserTBDto user);
    }
}
