﻿namespace MS.Client.SysInfoModule.ViewModels
{
    public class MenuManageViewModel:NavigationViewModel
    {
        private readonly IMenuService service;
        private readonly IDialogService dialogService;
        public MenuManageViewModel(IMenuService service, IDialogService dialogService,IRegionManager region,IEventAggregator aggregator,IContainer container):
            base(region,container,aggregator)
        {
            PageTitle = "菜单管理";
            Menus = new ObservableCollection<MenuDto>();
            this.service = service;
            this.dialogService = dialogService;
            FindCommand = new DelegateCommand(Find);
            AddCommand = new DelegateCommand<MenuDto>(AddNode);
            EditCommand = new DelegateCommand<MenuDto>(EditNode);
            DeleteCommond = new DelegateCommand<MenuDto>(DeleteNode);
            PageCommond = new DelegateCommand<Pagination>(PageChange);
        }

        private async void DeleteNode(MenuDto obj)
        {
            try
            {
                if (obj == null)
                {
                    MessageBox.Warning("请选择数据!","删除");
                    return;
                }
                var r = MessageBox.Show($"确认删除当前菜单：{obj.MenuName}", "操作提示", MessageBoxButton.OK, MessageBoxImage.Question);
                if (r == MessageBoxResult.OK)
                {
                    var result = await service.DeleteAsync(obj.MenuId);
                    if (result != null && result.Succeeded)
                    {
                        Menus.Remove(obj);
                        MessageBox.Show("删除成功");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void AddNode(MenuDto obj)
        {
            try
            {
                DialogParameters para = new DialogParameters();
                para.Add("Value", obj);
                dialogService.ShowDialog("AddMenuManageView", para, callback =>
                {
                    if (callback != null && callback.Result == ButtonResult.OK)
                    {
                        var model = callback.Parameters.GetValue<MenuDto>("Value");
                        if (model != null)
                        {
                            Menus.Add(model);
                        }
                    }
                });
            }
            catch (Exception ex)
            {

            }
        }

        private void EditNode(MenuDto obj)
        {
            try
            {
                if (obj == null)
                {
                    MessageBox.Warning("请选择一项数据:");
                    return;
                }
                DialogParameters para = new DialogParameters();
                para.Add("Value", obj);
                dialogService.ShowDialog("AddMenuManageView", para, callback =>
                {
                    if (callback != null && callback.Result == ButtonResult.OK)
                    {
                        var menu = callback.Parameters.GetValue<MenuDto>("Value");
                        if (menu != null)
                        {
                            var menuinfo = Menus.FirstOrDefault(x => x.MenuId == menu.MenuId);
                            if (menuinfo != null)
                            {
                                menuinfo.MenuName = menu.MenuName;  
                                menuinfo.MenuPath = menu.MenuPath;  
                                menuinfo.MenuIcon = menu.MenuIcon;  
                                menuinfo.ParentName = menu.ParentName;     
                            }
                        }
                    }
                });
            }
            finally {

            }
        }

        /// <summary>
        /// 刷新数据
        /// </summary>
        private void Refresh()
        {
            Find();
        }

        protected override async void Find()
        {
            try
            {
                ShowLoading();
                FindParameter findParameter = new  FindParameter() { PageIndex = PageIndex, PageSize = PageSize };
                var res = await service.GetPageListAsync(findParameter);
                if (res != null && res.Succeeded)
                {
                    var model = res.Data;
                    PageCount = Convert.ToInt32(model!.TotalCount) == 1 ? 1 : (int)Math.Ceiling(Convert.ToDouble(model!.TotalCount) / PageSize);
                    Menus.Clear();
                    foreach (var item in model.Items)
                    {
                        Menus.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                HideLoading();
            }
        }

        private ObservableCollection<MenuDto> menus;

        public ObservableCollection<MenuDto> Menus
        {
            get { return menus; }
            set { menus = value; RaisePropertyChanged(); }
        }
       
        public DelegateCommand FindCommand { get; set; }
        public DelegateCommand<MenuDto> AddCommand { get; set; }
        public DelegateCommand<MenuDto> EditCommand { get; set; }
        public DelegateCommand<MenuDto> DeleteCommond { get; set; }
    }
}
