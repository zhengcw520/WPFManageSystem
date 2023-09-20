namespace MS.Client.MainModule
{
    public class MainModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("LeftMenuTreeRegion", typeof(TreeMenuView));
            //regionManager.RegisterViewWithRegion("MainHeaderRegion", typeof(MainHeaderView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<Home, HomeViewModel>();
            containerRegistry.Register<TreeMenuView>();
            containerRegistry.Register<MainHeaderView>();
        }
    }
}
