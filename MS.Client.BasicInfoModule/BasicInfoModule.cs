using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System;
using System.Reflection;
using MS.Client.BasicInfoModule.ViewModels;
using MS.Client.BasicInfoModule.ViewModels.Dialogs;
using MS.Client.BasicInfoModule.Views;
using MS.Client.BasicInfoModule.Views.Dialogs;

namespace MS.Client.BasicInfoModule
{
    public class BasicInfoModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<UserManageView,UserManageViewModel>();
            containerRegistry.RegisterForNavigation<RoleManageView,RoleManageViewModel>();
            containerRegistry.RegisterForNavigation<GrantMenuView, GrantMenuViewModel>();
            containerRegistry.RegisterForNavigation<GrantUserView, GrantUserViewModel>();
            containerRegistry.RegisterForNavigation<GrantRoleView, GrantRoleViewModel>();
           
            containerRegistry.RegisterDialog<AddUserView>();
            containerRegistry.RegisterDialog<AddRoleView>();
        }
    }
}
