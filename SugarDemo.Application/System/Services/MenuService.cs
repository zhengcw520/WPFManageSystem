namespace SugarDemo.Application
{
    public class MenuService : IMenuService, IScoped
    {
        private readonly IRepository<MenuTB> _menuService;
        public MenuService(IRepository<MenuTB> menuService)
        {
            _menuService = menuService;
        }

        public async Task AddOrUpdateAsync(MenuDto model)
        {
            await _menuService.InsertOrUpdateAsync(model.Adapt<MenuTB>());
        }

        public async Task DeleteAsync(int id)
        {
            await _menuService.DeleteAsync(x => x.MenuId == id);
        }

        public async Task<List<MenuDto>> GetAllAsync()
        {
            var result = await _menuService.AsQueryable().ToListAsync();
            return result.Adapt<List<MenuDto>>();
        }

        public async Task<SqlSugarPagedList<MenuDto>> GetPageListAsync(FindParameter find)
        {
            var result = await _menuService.AsQueryable()
                .WhereIF(!string.IsNullOrEmpty(find.Search), it => it.MenuName!.Equals(find.Search))
                .OrderBy(u => u.MenuId)
                .ToPagedListAsync<MenuTB>(find.PageIndex, find.PageSize);
            return result.Adapt<SqlSugarPagedList<MenuDto>>();
        }

        public async Task<MenuDto> GetSingleAsync(int id)
        {
            var result = await _menuService.GetFirstAsync(x => x.MenuId.Equals(id));
            var dtoList = result.Adapt<MenuDto>();
            return dtoList;
        }
    }
}
