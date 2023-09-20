namespace MS.Client.SysInfoModule
{
    public class MenuItemModel
    {
        public int MenuId { get; set;}

        public string? MenuIcon
        {
            get;
            set;
        }

        public string? MenuName
        {
            get;set;    
        }

        public string? MenuPath
        {
            get; set;
        }

        public int State
        {
            get; set;
        }

        public int ParentId
        {
            get; set;
        }

        public ObservableCollection<MenuItemModel> Children
        {
            get;set;
        }

        public Boolean IsChecked
        {
            get; set;
        }
    }
}
