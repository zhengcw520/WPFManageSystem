namespace SugarDemo.Application
{
    public class RoleService : IRoleService, IScoped
    {
        private readonly IRepository<RoleTB> _roleService;
        private readonly IRepository<MenuTB> _menuService;
        private readonly IRepository<RoleTB> _userService;
        private readonly IRepository<RoleMenuTB> _roleMenuService;
        public RoleService(IRepository<RoleTB> roleService,
            IRepository<MenuTB> menuService,
            IRepository<RoleTB> useService,
            IRepository<RoleMenuTB> roleMenuService)
        {
            _roleService = roleService;
            _menuService = menuService;
            _userService = useService;
            _roleMenuService = roleMenuService;
        }

        /// <summary>
        /// 单个实体的新增与修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> AddOrUpdateAsync(RoleDto model)
        {
            return await _roleService.InsertOrUpdateAsync(model.Adapt<RoleTB>());
        }

        /// <summary>
        /// 单行的删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(int id)
        {
            return await _roleService.DeleteAsync(x => x.RoleId == id);
        }

        public async Task<List<RoleDto>> GetPageListAsync(FindParameter FindParameter)
        {
            Expression<Func<RoleTB, bool>> exp = Expressionable.Create<RoleTB>()
                .AndIF(!string.IsNullOrEmpty(FindParameter.Search), it => it.RoleName!.Equals(FindParameter.Search))
                .ToExpression();
            PageModel pageModel = new PageModel()
            {
                PageIndex = FindParameter.PageIndex,
                PageSize = FindParameter.PageSize,
            };
            var result = await _roleService.GetPageListAsync(exp, pageModel);
            var dtoList = result.Adapt<List<RoleDto>>();
            return dtoList;

        }

        public async Task<List<RoleDto>> GetAllAsync()
        {
            var result = await _roleService.GetListAsync();
            return result.Adapt<List<RoleDto>>();
        }

        public async Task<RoleDto> GetSingleAsync(int id)
        {
            var result = await _roleService.AsQueryable()
                .Includes(x => x.MenuList)
               .Where(x => x.RoleId == id).ToListAsync();
            return result.Adapt<RoleDto>();
        }

        public async Task<List<MenuDto>> GetMenusByRoleIdAsync(int roleId)
        {
            // 获取所有权限
            var result = await _menuService.AsQueryable()
                .LeftJoin<RoleMenuTB>((m, rm) => m.MenuId == rm.MenuId && rm.RoleId == roleId)
                .ToListAsync();
            var dtoList = result.Adapt<List<MenuDto>>();
            return dtoList;
        }

        public async Task<List<UserTBDto>> GetUsersByRoleIdAsync(int roleId)
        {
            var result = await _userService.AsQueryable()
                .LeftJoin<UserRoleTB>((u, ur) => ur.RoleId == roleId)
                .ToListAsync();
            var dtoList = result.Adapt<List<UserTBDto>>();
            return dtoList;
        }

        /// <summary>
        /// 批量插入角色菜单
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<bool> BatchInsertAsync(RoleBatchModel param)
        {
            // 获取所有权限
            return await _roleMenuService.InsertRangeAsync(param.AddModel?.Adapt<List<RoleMenuTB>>());
        }

        /// <summary>
        /// 批量更新角色菜单
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<bool> BatchUpdateAsync(RoleBatchModel param)
        {
            // 获取所有权限
            return await _roleMenuService.UpdateRangeAsync(param.DelModel?.Adapt<List<RoleMenuTB>>());
        }
        /// <summary>
        /// 批量更新角色菜单
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<bool> BatchUpdateRoleTBAsync(RoleBatchModel param)
        {
            // 获取所有权限
            Boolean IsFlag = false;
            if (param.AddModel?.Count > 0)
            {
                IsFlag = await _roleMenuService.InsertRangeAsync(param.AddModel?.Adapt<List<RoleMenuTB>>());
            }
            if (param.DelModel?.Count > 0)
            {
                //这里不是真正的删除更新状态(0 未删除,1标识删除)
                IsFlag = await _roleMenuService.UpdateRangeAsync(param.DelModel?.Adapt<List<RoleMenuTB>>());
            }
            if (param.Model != null)
            {
                IsFlag = await _roleService.UpdateAsync(param.Model?.Adapt<RoleTB>());
            }
            return IsFlag;
        }
    }
}
