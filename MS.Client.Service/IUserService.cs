namespace MS.Client.Service
{
    public interface IUserService :IBaseService<UserTBDto>
    {
        Task<FurApiResponse> GetMenusByUserIdAsync(int id);
        Task<FurApiResponse> GetRolesByUserIdAsync(int id);
        Task<FurApiResponse> BatchUserRolesAsync(UserBatchModel param);
    }
}
