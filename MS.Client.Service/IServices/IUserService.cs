namespace MS.Client.Service
{
    public interface IUserService : IBaseService<UserDto>
    {
        Task<FurApiResponse<List<MenuDto>>> GetMenusByUserIdAsync(int id);
        Task<FurApiResponse<List<RoleDto>>> GetRolesByUserIdAsync(int id);
        Task<FurApiResponse<bool>> BatchUserRolesAsync(UserBatchModel param);
    }
}
