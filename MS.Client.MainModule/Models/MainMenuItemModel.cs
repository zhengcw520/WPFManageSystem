using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MS.Client.MainModule.Models
{
    public class MainMenuItemModel : BindableBase
    {
        private readonly IRegionManager _regionManager; 
        public MainMenuItemModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            OpenViewCommand = new DelegateCommand<MainMenuItemModel>(Open);
        }

        public DelegateCommand<MainMenuItemModel> OpenViewCommand { get; set; }

        #region 属性
        private string? menuIcon;

        public string? MenuIcon
        {
            get { return menuIcon; }
            set { menuIcon = value;RaisePropertyChanged(); }
        }

        private string? menuName;

        public string? MenuName
        {
            get { return menuName; }
            set { menuName = value; RaisePropertyChanged(); }
        }

        private string? menuPath;

        public string? MenuPath
        {
            get { return menuPath; }
            set { menuPath = value; RaisePropertyChanged(); }
        }

        private string? parentName;

        public string? ParentName
        {
            get { return parentName; }
            set { parentName = value; RaisePropertyChanged(); }
        }

        private bool contractionTemplate;
        /// <summary>
        /// 收缩面板-模板
        /// </summary>
        public bool ContractionTemplate
        {
            get { return contractionTemplate; }
            set { contractionTemplate = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<MainMenuItemModel> children;

        public ObservableCollection<MainMenuItemModel> Children
        {
            get { return children; }
            set { children = value; RaisePropertyChanged(); }
        }

        private bool isExpanded;

        public bool IsExpanded
        {
            get { return isExpanded; }
            set { isExpanded = value; RaisePropertyChanged(); }
        }
        #endregion

        #region 方法
        private void Open(MainMenuItemModel model)
        {
            if ((model.Children == null || model.Children.Count == 0) &&
                        !string.IsNullOrEmpty(model.MenuPath))
                _regionManager.RequestNavigate("MainContentRegion", model.MenuPath);
            else
                IsExpanded = !IsExpanded;
        }
        #endregion
    }
}
