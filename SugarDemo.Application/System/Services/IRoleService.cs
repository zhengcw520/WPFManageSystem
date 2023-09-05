namespace SugarDemo.Application
{
    public interface IRoleService : IBaseService<RoleDto>
    {
        Task<List<MenuDto>> GetMenusByRoleIdAsync(int roleId);
        Task<List<UserTBDto>> GetUsersByRoleIdAsync(int roleId);
        Task<bool> BatchUpdateAsync(RoleBatchModel model);
        Task<bool> BatchInsertAsync(RoleBatchModel model);
    }
}
