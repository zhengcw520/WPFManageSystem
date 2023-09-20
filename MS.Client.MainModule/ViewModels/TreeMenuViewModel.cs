namespace MS.Client.MainModule.ViewModels
{
    internal class TreeMenuViewModel : BindableBase
    {
        private List<MenuDto> origMenus = null;

        private IRegionManager _regionManager = null;

        private ObservableCollection<MainMenuItemModel> menus;

        public ObservableCollection<MainMenuItemModel> Menus
        {
            get { return menus; }
            set { menus = value;RaisePropertyChanged(); }
        }

        public TreeMenuViewModel(IContainerProvider containerProvider, IRegionManager regionManager)
        {
            _regionManager = regionManager;
            //var global = containerProvider.Resolve<GlobalEntity>();// 注册一个单例
            if (GlobalEntity.MenuDtos!.Count >0)
            {
                origMenus = GlobalEntity.MenuDtos;
                Menus = new ObservableCollection<MainMenuItemModel>();
                this.FillMenus(Menus,null);
            }
        }

        ///递归
        ///
        private void FillMenus(ObservableCollection<MainMenuItemModel> menus, string parentname)
        {
            var sub = origMenus.Where(m => m.ParentName == parentname);

            if (sub.Count() > 0)
            {
                foreach (var item in sub)
                {
                    MainMenuItemModel mm = new MainMenuItemModel(_regionManager)
                    {
                        MenuName = item.MenuName,
                        MenuIcon = item.MenuIcon,
                        MenuPath = item.MenuPath,
                        ParentName = item.ParentName,  
                    };
                    menus.Add(mm);

                    FillMenus(mm.Children = new ObservableCollection<MainMenuItemModel>(), item.MenuName);
                }
            }
        }
    }
}
