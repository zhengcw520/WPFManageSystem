namespace SugarDemo.Application
{
    /// <summary>
    /// 菜单服务接口
    /// </summary>
    public class MenuAppService : IDynamicApiController
    {
        private readonly IMenuService _service;
        public MenuAppService(IMenuService service)
        {
            _service = service;
        }
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="FindParameter"></param>
        /// <returns></returns>
        public async Task<SqlSugarPagedList<MenuDto>> GetPageListAsync(FindParameter FindParameter)
        {
           return await _service.GetPageListAsync(FindParameter);
        }

        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns></returns>
        public async Task<List<MenuDto>> GetAllAsync()
        {
            return await _service.GetAllAsync();
        }

        /// <summary>
        /// 获取单个数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<MenuDto> GetSingleAsync(int id)
        {
            return await _service.GetSingleAsync(id);
        }

        /// <summary>
        /// 新增修改数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task AddOrUpdateAsync(MenuDto model) 
        { 
           await _service.AddOrUpdateAsync(model);
        }

        /// <summary>
        /// 删除单个菜单数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteAsync(int id) 
        { 
            await _service.DeleteAsync(id);
        } 
    }
}
