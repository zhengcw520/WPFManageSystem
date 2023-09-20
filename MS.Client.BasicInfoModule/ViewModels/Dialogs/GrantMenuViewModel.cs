namespace MS.Client.BasicInfoModule.ViewModels.Dialogs
{
    public class GrantMenuViewModel : BindableBase, IDialogAware
    {
        public string Title { get; set; } = "分配菜单";

        public event Action<IDialogResult> RequestClose;

        private ObservableCollection<MenuDto> menus;
        /// <summary>
        /// 在编辑画面显示的菜单
        /// </summary>
        public ObservableCollection<MenuDto> Menus
        {
            get { return menus; }
            set { menus = value; RaisePropertyChanged(); }
        }

        private RoleDto current;

        public RoleDto Current
        {
            get { return current; }
            set { current = value;RaisePropertyChanged(); }
        }


        private readonly IRoleService service;
        private readonly IMenuService menuService;
        public GrantMenuViewModel(IRoleService _service, IMenuService _menuService)
        {
            Menus = new ObservableCollection<MenuDto>();
            service = _service;
            menuService = _menuService;
            btnOKCommand = new DelegateCommand(Save);
            btnCancelCommand = new DelegateCommand(Cancel);
        }

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {

        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            if (parameters.ContainsKey("Value"))
            {
                Current = parameters.GetValue<RoleDto>("Value");
                if (Current != null) GetDataById(Current.RoleId);
            }
        }

        List<RoleMenuDto> roleMenuEntities = new List<RoleMenuDto>();
        private async void GetDataById(int id)
        {
            var rolemenu = await service.GetMenusByRoleIdAsync(id);
            if (rolemenu != null && rolemenu.Succeeded)
            {
                roleMenuEntities.AddRange(rolemenu.Data);
            }
            var resultMenu = await menuService.GetAllAsync();//后期从xml文件加载
            if (resultMenu != null && resultMenu.Succeeded)
            {   
                Menus.Clear();
                foreach (var item in resultMenu.Data)
                {
                    if (roleMenuEntities != null && roleMenuEntities.Any(x => x.MenuId == item.MenuId && x.State == 0))
                    {
                        item.IsSelected = true;
                    }
                    else
                    {
                        item.IsSelected = false;
                    }
                    Menus.Add(item);
                }  
            }
        }

        private async void Save()
        {
            List<MenuDto> currentListMenus = new List<MenuDto>();
            currentListMenus.AddRange(Menus);
            List<RoleMenuDto> AddList = new List<RoleMenuDto>();
            List<RoleMenuDto> UpdList = new List<RoleMenuDto>();
            currentListMenus.ForEach(x =>
            {
                //删除数据
                if ((roleMenuEntities.Exists(y => y.MenuId == x.MenuId && y.State ==0) && !x.IsSelected))
                {
                    UpdList.Add(new RoleMenuDto()
                    {
                        RoleId = Current.RoleId,
                        MenuId = x.MenuId,
                        State = 1,
                        CreateBy = GlobalEntity.UserName,  
                        CreateDate = x.CreateDate,  
                        
                    });
                }
                if(roleMenuEntities.Exists(y => y.MenuId == x.MenuId && y.State == 1) && x.IsSelected)
                {
                    UpdList.Add(new RoleMenuDto()
                    {
                        RoleId = Current.RoleId,
                        MenuId = x.MenuId,
                        State = 0,
                        CreateBy = GlobalEntity.UserName,
                        CreateDate = x.CreateDate,
                    });
                }
                if (!roleMenuEntities.Exists(y => y.MenuId == x.MenuId) && x.IsSelected)
                {
                    AddList.Add(new RoleMenuDto()
                    {
                        RoleId = Current.RoleId,
                        MenuId = x.MenuId,
                        State = 0,
                        CreateBy = GlobalEntity.UserName,
                        CreateDate = x.CreateDate,
                    });
                }
            });
            RoleBatchModel batchModel = new RoleBatchModel() { AddModel = AddList, DelModel = UpdList, Model = null };
            var result = await service.BatchUpdateRoleMenuAsync(batchModel);
            if (result != null && result.Succeeded)
                RequestClose?.Invoke(new DialogResult(ButtonResult.OK));
            else
            {
                MessageBox.Show("保存失败，请联系管理员！");
                Cancel();
            }
        }

        private void Cancel()
        {
            RequestClose?.Invoke(new DialogResult(ButtonResult.Cancel));
        }

        public DelegateCommand btnOKCommand { get; set; }
        public DelegateCommand btnCancelCommand { get; set; }
    }
}
