namespace SugarDemo.Application
{
    public class UserAppService : IDynamicApiController
    {
        private readonly IUserService _service;
        public UserAppService(IUserService service)
        {
            _service = service;
        }
        public async Task<List<UserTBDto>> GetPageListAsync([FromQuery]FindParameter FindParameter) 
        {
            return await _service.GetPageListAsync(FindParameter);
        }
        public async Task<List<UserTBDto>> GetAllAsync() 
        {
            return await _service.GetAllAsync();
        }
        public async Task<UserTBDto> GetSingleAsync(int id) 
        { 
            return await _service.GetSingleAsync(id); 
        }
        public async Task<bool> AddOrUpdateAsync(UserTBDto model)
        { 
            return await _service.AddOrUpdateAsync(model);
        }
        public async Task<bool> DeleteAsync(int id)
        {
           return await _service.DeleteAsync(id);
        }
        public Task<List<MenuDto>> GetMenusByUserIdAsync(int id) 
        {
            return _service.GetMenusByUserIdAsync(id);
        }
        public Task<List<RoleDto>> GetRolesByUserIdAsync(int id) 
        { 
            return _service.GetRolesByUserIdAsync(id);
        } 
    }
}
