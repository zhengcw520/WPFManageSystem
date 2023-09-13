namespace MS.Client.DAL
{
    public interface IUserDAL :IBaseDAL<UserTBDto>
    {
        Task<FurApiResponse> GetMenusByUserIdAsync(int id);
        Task<FurApiResponse> GetRolesByUserIdAsync(int id);
        //Task<FurApiResponse> BatchUpdateUserRoleInfoAsync(UserBatchModel param);
    }
}
