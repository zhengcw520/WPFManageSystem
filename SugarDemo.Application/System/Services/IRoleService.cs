namespace SugarDemo.Application
{
    public interface IRoleService : IBaseService<RoleDto>
    {
        Task<List<MenuDto>> GetMenusByRoleIdAsync(int roleId);
        Task<List<UserDto>> GetUsersByRoleIdAsync(int roleId);
        Task BatchUpdateAsync(RoleBatchModel model);
        Task BatchInsertAsync(RoleBatchModel model);

        Task BatchUpdateRoleMenuAsync(RoleBatchModel model);
    }
}
