﻿namespace SugarDemo.Application
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

        public async Task AddOrUpdateAsync(UserDto model)
        {
            await _userService.InsertOrUpdateAsync(model.Adapt<UserTB>());
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _userService!.GetFirstAsync(x => x.UserId.Equals(id));
            await _userService.DeleteAsync(user);
        }

        public async Task<SugarPagedList<UserDto>> GetPageListAsync(FindParameter find)
        {
            var result = await _userService.AsQueryable()
                .WhereIF(!string.IsNullOrEmpty(find.Search), it => it.UserName!.Equals(find.Search))
                .OrderBy(u => u.UserId)
                .ToPagedListAsync<UserTB>(find.PageIndex, find.PageSize);
            return result.Adapt<SugarPagedList<UserDto>>();
        }

        public async Task<List<UserDto>> GetAllAsync()
        {
            var result = await _userService.AsQueryable().ToListAsync();
            return result.Adapt<List<UserDto>>();
        }

        public async Task<UserDto> GetSingleAsync(int id)
        {
            var result = await _userService.AsQueryable()
                      .Includes(x => x.RoleList)
                      .Where(x => x.UserId == id)
                      .ToListAsync();
            return result.Adapt<UserDto>();
        }

        public async Task<List<MenuDto>> GetMenusByUserIdAsync(int userid)
        {
            var result = await _menuService.AsQueryable()
                .LeftJoin<RoleMenuTB>((m, rm) => m.MenuId == rm.MenuId)
                .LeftJoin<UserRoleTB>((m, rm, ur) => rm.RoleId == ur.RoleId)
                .Where((m, rm, ur)=> ur.RoleId == userid)
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
                .LeftJoin<UserTB>((r, ur, u) => ur.UserId == u.UserId)
                .Where((r, ur, u)=> u.UserId == userid)
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
