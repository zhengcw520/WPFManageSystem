namespace SugarDemo.Application
{
    public interface IUserService:IBaseService<UserDto>
    {
        Task<List<MenuDto>> GetMenusByUserIdAsync(int id);
        Task<List<RoleDto>> GetRolesByUserIdAsync(int id);
    }
}
