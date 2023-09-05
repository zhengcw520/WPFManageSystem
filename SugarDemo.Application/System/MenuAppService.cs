namespace SugarDemo.Application
{
    public class MenuAppService : IDynamicApiController
    {
        private readonly IMenuService _service;
        public MenuAppService(IMenuService service)
        {
            _service = service;
        }
        public async Task<List<MenuDto>> GetPageListAsync([FromQuery] FindParameter FindParameter)
        {
           return await _service.GetPageListAsync(FindParameter);
        }

        public async Task<List<MenuDto>> GetAllAsync() 
        {
            return await _service.GetAllAsync();
        }
        public async Task<MenuDto> GetSingleAsync(int id)
        {
            return await _service.GetSingleAsync(id);
        }
        public async Task<bool> AddOrUpdateAsync(MenuDto model) 
        { 
           return await _service.AddOrUpdateAsync(model);
        }
        public async Task<bool> DeleteAsync(int id) 
        { 
            return await _service.DeleteAsync(id);
        } 
    }
}
