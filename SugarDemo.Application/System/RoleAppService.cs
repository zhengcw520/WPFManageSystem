namespace SugarDemo.Application
{
    /// <summary>
    /// 角色服务接口
    /// </summary>
    [DisplayName("角色服务接口")]
    public class RoleAppService : IDynamicApiController
    {
        private readonly IRoleService _service;
        public RoleAppService(IRoleService service)
        {
            _service = service;
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="FindParameter"></param>
        /// <returns></returns>
        [DisplayName("角色获取分页数据")]
        public async Task<SqlSugarPagedList<RoleDto>> GetPageListAsync(FindParameter FindParameter)
        {
            return await _service.GetPageListAsync(FindParameter);
        }

        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns></returns>
        [DisplayName("角色获取所有数据")]
        public async Task<List<RoleDto>> GetAllAsync()
        {
            return await _service.GetAllAsync();
        }

        /// <summary>
        /// 获取单个用户数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [DisplayName("角色获取单个用户数据")]
        public async Task<RoleDto> GetSingleAsync(int id)
        {
            return await _service.GetSingleAsync(id);
        }

        /// <summary>
        /// 新增或修改数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [DisplayName("新增或修改数据")]
        public async Task AddOrUpdateAsync(RoleDto model)
        {
            await _service.AddOrUpdateAsync(model);
        }

        /// <summary>
        /// 删除单条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [DisplayName("删除单条数据")]
        public async Task DeleteAsync(int id)
        {
            await _service.DeleteAsync(id);
        }

        /// <summary>
        /// 根据角色id获取角色用户的菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [DisplayName("获取角色用户的菜单")]
        public async Task<List<MenuDto>> GetMenusByRoleIdAsync(int id)
        {
            return await _service.GetMenusByRoleIdAsync(id);
        }

        /// <summary>
        /// 根据角色获取所拥有的用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [DisplayName("根据角色获取所拥有的用户")]
        public async Task<List<UserTBDto>> GetUsersByRoleIdAsync(int id)
        {
            return await _service.GetUsersByRoleIdAsync(id);
        }

        /// <summary>
        /// 批量更新角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [DisplayName("批量更新角色")]
        public async Task BatchUpdateAsync(RoleBatchModel model)
        {
            await _service.BatchUpdateAsync(model);
        }

        /// <summary>
        /// 批量插入角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [DisplayName("批量插入角色")]
        public async Task BatchInsertAsync(RoleBatchModel model) 
        {
            await _service.BatchInsertAsync(model);
        } 
    }
}
