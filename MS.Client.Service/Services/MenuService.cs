namespace MS.Client.Service
{
    public class MenuService : BaseService<MenuDto>, IMenuService
    {
        private readonly string serviceName = "Menu";
        private readonly string ApiName = "github";

        public MenuService() : base("Menu", "github")
        {
        }
    }
}
