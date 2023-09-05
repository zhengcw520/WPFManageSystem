namespace SugarDemo.Application
{
    public interface IUserService:IBaseService<UserTBDto>
    {
        Task<List<MenuDto>> GetMenusByUserIdAsync(int id);
        Task<List<RoleDto>> GetRolesByUserIdAsync(int id);
    }
}
