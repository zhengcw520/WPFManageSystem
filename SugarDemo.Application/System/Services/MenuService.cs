namespace SugarDemo.Application
{
    public class MenuService : IMenuService,IScoped
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

        public async Task<List<MenuDto>> GetPageListAsync(FindParameter FindParameter)
        {
            Expression<Func<MenuTB, bool>> exp = Expressionable.Create<MenuTB>()
                .AndIF(!string.IsNullOrEmpty(FindParameter.Search), it => it.MenuName!.Equals(FindParameter.Search))
                .ToExpression();
            PageModel pageModel = new PageModel()
            {
                PageIndex = FindParameter.PageIndex,
                PageSize = FindParameter.PageSize,
            };
            //SqlSuagr分页是从1开始的不是0开始
            var result = await _menuService.GetPageListAsync(exp, pageModel);
            var dtoList = result.Adapt<List<MenuDto>>();
            return dtoList;
        }

        public async Task<MenuDto> GetSingleAsync(int id)
        {
            var result = await _menuService.GetFirstAsync(x => x.MenuId.Equals(id));
            var dtoList = result.Adapt<MenuDto>();
            return dtoList;
        }

        public async Task<List<MenuDto>> GetAllAsync()
        {
            var result = await _menuService.GetListAsync();
            var dtoList = result.Adapt<List<MenuDto>>();
            return dtoList;
        }
    }
}
