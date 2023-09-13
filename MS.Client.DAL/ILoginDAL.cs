namespace MS.Client.DAL
{
    public interface ILoginDAL
    {
        Task<FurApiResponse> Login(UserTBDto user);

        Task<FurApiResponse> Resgiter(UserTBDto user);
    }
}
