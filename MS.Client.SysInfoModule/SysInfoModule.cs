using Prism.Ioc;
using Prism.Modularity;
using MS.Client.SysInfoModule.Views;
using MS.Client.SysInfoModule.Views.Dialog;

namespace MS.Client.SysInfoModule
{
    public class SysInfoModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MenuManageView>();
            containerRegistry.RegisterForNavigation<FileUploadView>();

            containerRegistry.RegisterDialog<AddMenuManageView>();
        }
    }
}
