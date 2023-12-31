﻿namespace MS.Client.BasicInfoModule.ViewModels
{
    public class RoleManageViewModel:NavigationViewModel
    {
        #region 私有属性
        private readonly IRoleService roleService;
        private readonly IDialogService dialogService;
        private readonly IRegionManager regionManager;
        #endregion

        #region 属性
        private ObservableCollection<RoleDto> roles;
        public ObservableCollection<RoleDto> Roles
        {
            get { return roles; }
            set { roles = value; RaisePropertyChanged(); }
        }

        public DelegateCommand<RoleDto> EditCommand { get; set; }
        //public DelegateCommand<RoleDto> AddCommand { get; set; }
        #endregion

        public RoleManageViewModel(IDialogService _dialogService,IRoleService _roleService, 
            IEventAggregator _eventAggregator, IRegionManager _regionManager, IContainer container):
            base(_regionManager,container, _eventAggregator)
        {
            PageTitle = "角色管理";
            Roles = new ObservableCollection<RoleDto>();
            roleService = _roleService;
            dialogService = _dialogService;
            regionManager = _regionManager;
            EditCommand = new DelegateCommand<RoleDto>(Edit);
            GrantUserCommond = new DelegateCommand<RoleDto>(GrantUser);
            GrantMenuCommond = new DelegateCommand<RoleDto>(GrantMenu);
        }
        private void GrantUser(RoleDto obj)
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
                dialogService.ShowDialog("GrantUserView", para, callback =>
                {
                    if (callback != null && callback.Result == ButtonResult.OK)
                    {
                        MessageBox.Show("保存成功");
                        Refresh();
                    }
                });
            }
            catch (Exception ex)
            {

            }
        }     
        
        private void GrantMenu(RoleDto obj)
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
                dialogService.ShowDialog("GrantMenuView", para, callback =>
                {
                    if (callback != null && callback.Result == ButtonResult.OK)
                    {
                        MessageBox.Show("保存成功");
                        Refresh();
                    }
                });
            }
            catch (Exception ex)
            {

            }
        }

        private void Edit(RoleDto obj)
        {
            try
            {
                if (obj == null)
                {
                    MessageBox.Warning("请选择一项数据:");
                    return;
                }
                DialogParameters para = new DialogParameters();
                para.Add("OperType", OperTypeEnum.Edit);
                para.Add("Data", obj);
                dialogService.ShowDialog("AddRoleView", para, callback =>
                {
                    if (callback != null && callback.Result == ButtonResult.OK)
                    {
                        MessageBox.Show("保存成功");
                    }
                });
            }
            catch (Exception ex)
            {

            }
        }

        private void Refresh()
        {
            Find();
        }

        protected override void Add()
        {
            try
            {
                DialogParameters para = new DialogParameters();
                para.Add("OperType", OperTypeEnum.Add);
                dialogService.ShowDialog("AddRoleView", para, callback =>
                {
                    if (callback != null && callback.Result == ButtonResult.OK)
                    {
                        MessageBox.Show("保存成功");
                    }
                });
            }
            catch (Exception ex)
            {

            }
        }

        protected override async void Find()
        {
            try
            {
                ShowLoading();
                FindParameter findParameter = new FindParameter() { PageIndex = PageIndex, PageSize = PageSize, Search = "" };
                var res = await roleService.GetPageListAsync(findParameter);
                if (res != null && res.Succeeded)
                {
                    var model = res.Data;
                    PageCount = Convert.ToInt32(model!.TotalCount) == 1 ? 1 : (int)Math.Ceiling(Convert.ToDouble(model!.TotalCount) / PageSize);
                    Roles.Clear();
                    foreach (var item in model.Items)
                    {
                        Roles.Add(item);
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

        public DelegateCommand<RoleDto> GrantUserCommond { get; set; }
        public DelegateCommand<RoleDto> GrantMenuCommond { get; set; }
    }
}
