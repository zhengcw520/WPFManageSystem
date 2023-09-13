namespace SugarDemo.Application
{
    public class UserService : IUserService, IScoped
    {
        private readonly IRepository<UserTB> _userService;
        private readonly IRepository<MenuTB> _menuService;
        private readonly IRepository<RoleTB> _roleService;
        private readonly IRepository<UserRoleTB> _userroleService;
        public UserService(IRepository<UserTB> userService,
            IRepository<MenuTB> menuService,
            IRepository<RoleTB> roleService,
            IRepository<UserRoleTB> userroleService)
        {
            _userService = userService;
            _menuService = menuService;
            _roleService = roleService;
            _userroleService = userroleService;
        }
        public string GetDescription()
        {
            return "让 .NET 开发更简单，更通用，更流行。";
        }

        public async Task AddOrUpdateAsync(UserTBDto model)
        {
            await _userService.InsertOrUpdateAsync(model.Adapt<UserTB>());
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _userService!.GetFirstAsync(x => x.UserId.Equals(id));
            await _userService.DeleteAsync(user);
        }

        public async Task<SqlSugarPagedList<UserTBDto>> GetPageListAsync(FindParameter find)
        {
            var result = await _userService.AsQueryable()
                .WhereIF(!string.IsNullOrEmpty(find.Search), it => it.UserName!.Equals(find.Search))
                .OrderBy(u => u.UserId)
                .ToPagedListAsync<UserTB>(find.PageIndex, find.PageSize);
            return result.Adapt<SqlSugarPagedList<UserTBDto>>();
        }

        public async Task<List<UserTBDto>> GetAllAsync()
        {
            var result = await _userService.AsQueryable().ToListAsync();
            return result.Adapt<List<UserTBDto>>();
        }

        public async Task<UserTBDto> GetSingleAsync(int id)
        {
            var result = await _userService.AsQueryable()
                      .Includes(x => x.RoleList)
                     .Where(x => x.UserId == id).ToListAsync();
            return result.Adapt<UserTBDto>();
        }

        public async Task<List<MenuDto>> GetMenusByUserIdAsync(int userid)
        {
            var result = await _menuService.AsQueryable()
                .LeftJoin<RoleMenuTB>((m, rm) => m.MenuId == rm.MenuId)
                .LeftJoin<UserRoleTB>((m, rm, ur) => rm.RoleId == ur.RoleId && ur.RoleId == userid)
                .ToListAsync();
            return result.Adapt<List<MenuDto>>();
        }

        /// <summary>
        ///  根据用户Id获取所有角色
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public async Task<List<RoleDto>> GetRolesByUserIdAsync(int userid)
        {
            var result = await _roleService.AsQueryable()
                .LeftJoin<UserRoleTB>((r, ur) => r.RoleId == ur.RoleId)
                .LeftJoin<UserTB>((r, ur, u) => ur.UserId == u.UserId && u.UserId == userid)
                .ToListAsync();
            return result.Adapt<List<RoleDto>>();
        }

        /// <summary>
        /// 批量更新用户角色菜单
        /// </summary>
        /// <param name="modelPa"></param>
        /// <returns></returns>
        public async Task BatchUpdateUserRoleTBAsync(UserBatchModel modelPa)
        {

            if (modelPa.AddModel?.Count > 0)
            {
                var model = modelPa.AddModel.Adapt<List<UserRoleTB>>();
                await _userroleService.InsertRangeAsync(model);
            }
            if (modelPa.DelModel?.Count > 0)
            {
                //这里不是真正的删除更新状态(0 未删除,1标识删除)
                var model = modelPa.DelModel.Adapt<List<UserRoleTB>>();
                await _userroleService.UpdateRangeAsync(model);
            }
            if (modelPa.Model != null)
            {
                var model = modelPa.Model.Adapt<UserTB>();
                await _userService!.UpdateAsync(model);
            }
        }
    }
}
