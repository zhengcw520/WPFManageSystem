namespace SugarDemo.Application
{
    public class RoleAppService : IDynamicApiController
    {
        private readonly IRoleService _service;
        public RoleAppService(IRoleService service)
        {
            _service = service;
        }

        public async Task<List<RoleDto>> GetPageListAsync([FromQuery] FindParameter FindParameter)
        {
            return await _service.GetPageListAsync(FindParameter);
        }
        public async Task<List<RoleDto>> GetAllAsync()
        {
            return await _service.GetAllAsync();
        }
        public async Task<RoleDto> GetSingleAsync(int id)
        {
            return await _service.GetSingleAsync(id);
        }
        public async Task<bool> AddOrUpdateAsync(RoleDto model)
        {
            return await _service.AddOrUpdateAsync(model);
        }
        public async Task<bool> DeleteAsync(int id)
        {
            return await _service.DeleteAsync(id);
        }
        public async Task<List<MenuDto>> GetMenusByRoleIdAsync(int id)
        {
            return await _service.GetMenusByRoleIdAsync(id);
        }
        public async Task<List<UserTBDto>> GetUsersByRoleIdAsync(int id)
        {
            return await _service.GetUsersByRoleIdAsync(id);
        }
        public async Task<bool> BatchUpdateAsync(RoleBatchModel model)
        {
            return await _service.BatchUpdateAsync(model);
        }
        public async Task<bool> BatchInsertAsync(RoleBatchModel model) 
        {
            return await _service.BatchInsertAsync(model);
        } 
    }
}
