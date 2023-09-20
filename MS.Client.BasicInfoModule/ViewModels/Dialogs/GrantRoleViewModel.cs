namespace MS.Client.BasicInfoModule.ViewModels.Dialogs
{
    public class GrantRoleViewModel : BindableBase, IDialogAware
    {
        public string Title { get; set; } = "分配角色";

        public event Action<IDialogResult> RequestClose;

        private ObservableCollection<RoleDto> roles;
        /// <summary>
        /// 在编辑画面显示的菜单
        /// </summary>
        public ObservableCollection<RoleDto> Roles
        {
            get { return roles; }
            set { roles = value; RaisePropertyChanged(); }
        }

        private UserDto current;

        public UserDto Current
        {
            get { return current; }
            set { current = value; RaisePropertyChanged(); }
        }

        private readonly IUserService service;
        private readonly IRoleService roleService;
        public GrantRoleViewModel(IUserService _service, IRoleService _roleService)
        {
            Roles = new ObservableCollection<RoleDto>();
            service = _service;
            btnOKCommand = new DelegateCommand(Save);
            btnCancelCommand = new DelegateCommand(Cancel);
            roleService = _roleService;
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
                Current = parameters.GetValue<UserDto>("Value");
                if (Current != null) GetDataById(Current.UserId);
            }
        }

        List<RoleDto> roleDto = new List<RoleDto>();
        private async void GetDataById(int id)
        {
            //用户ID获取角色
            var result = await service.GetRolesByUserIdAsync(id);
            if (result != null && result.Succeeded)
            {
                roleDto.AddRange(result.Data);
            }
            //获取所有角色
            var resultMenu = await roleService.GetAllAsync();
            if (resultMenu != null && resultMenu.Succeeded)
            {
                Roles.Clear();
                foreach (var item in resultMenu.Data)
                {
                    if (roleDto.Any(x=>x.RoleId == item.RoleId))
                    {
                        item.IsSelected = true;
                    }
                    else
                    {
                        item.IsSelected = false;
                    }
                    Roles.Add(item);
                }
            }
        }

        private async void Save()
        {
            List<RoleDto> currentListRoles = new List<RoleDto>();
            currentListRoles.AddRange(Roles);
            List<UserRoleDto> AddList = new List<UserRoleDto>();
            List<UserRoleDto> UpdList = new List<UserRoleDto>();
            currentListRoles.ForEach(x =>
            {
                //删除数据
                if ((roleDto.Exists(y => y.RoleId == x.RoleId && y.IsDel == 0) && !x.IsSelected))
                {
                    UpdList.Add(new UserRoleDto()
                    {
                        RoleId = Current.UserId,
                        UserId = x.RoleId,
                        State = 1,
                        CreateBy = "admin",
                        CreateDate = DateTime.Now
                    });
                }
                //设置逻辑删除后，删除的数据就无法查询出来了，这种方式不生效
                //if (roleDto.Exists(y => y.RoleId == x.RoleId && y.IsDel == 1) && x.IsSelected)
                //{
                //    UpdList.Add(new UserRoleDto()
                //    {
                //        RoleId = Current.UserId,
                //        UserId = x.RoleId,
                //        State = 0,
                //        CreateBy = "admin",
                //        CreateDate = DateTime.Now
                //    });
                //}
                if (!roleDto.Exists(y => y.RoleId == x.RoleId) && x.IsSelected)
                {
                    AddList.Add(new UserRoleDto()
                    {
                        RoleId = Current.UserId,
                        UserId = x.RoleId,
                        State = 0,
                        CreateBy = "admin",
                        CreateDate = DateTime.Now
                    });
                }
            });
            UserBatchModel batchModel = new UserBatchModel() { AddModel = AddList, DelModel = UpdList, Model = null };
            var result = await service.BatchUserRolesAsync(batchModel);
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
