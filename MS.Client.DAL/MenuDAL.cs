namespace MS.Client.DAL
{
    public class MenuDAL : BaseDAL<MenuDto>, IMenuDAL
    {
        private readonly string DALName = "Menu";
        private readonly string ApiName = "github";

        public MenuDAL() : base("Menu","github")
        {
        }
    }
}
