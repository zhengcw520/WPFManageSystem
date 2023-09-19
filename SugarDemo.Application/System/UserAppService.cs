namespace SugarDemo.Application
{
    /// <summary>
    /// 用户服务接口
    /// </summary>
    [DisplayName("用户服务")]
    public class UserAppService : IDynamicApiController
    {
        private readonly IUserService _service;
        public UserAppService(IUserService service)
        {
            _service = service;
        }
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="FindParameter"></param>
        /// <returns></returns>
        [DisplayName("获取用户分页数据")]
        public async Task<SqlSugarPagedList<UserTBDto>> GetPageListAsync([FromQuery]FindParameter FindParameter)
        {
            return await _service.GetPageListAsync(FindParameter);
        }

        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns></returns>
        [DisplayName("获取用户所有数据")]
        public async Task<List<UserTBDto>> GetAllAsync()
        {
            return await _service.GetAllAsync();
        }

        /// <summary>
        /// 通过Id获取单个数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [DisplayName("获取单个用户数据")]
        public async Task<UserTBDto> GetSingleAsync(int id) 
        { 
            return await _service.GetSingleAsync(id); 
        }

        /// <summary>
        /// 新增或更新数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [DisplayName("新增或更新数据")]
        public async Task AddOrUpdateAsync(UserTBDto model)
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
        /// 获取单个用户拥有的菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [DisplayName("获取单个用户拥有的菜单")]
        public Task<List<MenuDto>> GetMenusByUserIdAsync(int id) 
        {
            return _service.GetMenusByUserIdAsync(id);
        }

        /// <summary>
        /// 获取单个用户拥有的角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [DisplayName("获取单个用户拥有的角色")]
        public Task<List<RoleDto>> GetRolesByUserIdAsync(int id) 
        { 
            return _service.GetRolesByUserIdAsync(id);
        } 
    }
}
