namespace MS.Client.BasicInfoModule.ViewModels.Dialogs
{
    public class GrantUserViewModel : BindableBase, IDialogAware
    {
        public string Title { get; set; } = "分配用户";

        public event Action<IDialogResult> RequestClose;

        private ObservableCollection<UserDto> users;
        /// <summary>
        /// 在编辑画面显示的菜单
        /// </summary>
        public ObservableCollection<UserDto> Users
        {
            get { return users; }
            set { users = value; RaisePropertyChanged(); }
        }

        private RoleDto current;

        public RoleDto Current
        {
            get { return current; }
            set { current = value; RaisePropertyChanged(); }
        }

        private readonly IUserService service;
        private readonly IRoleService roleService;
        public GrantUserViewModel(IUserService _service, IRoleService _roleService)
        {
            Users = new ObservableCollection<UserDto>();
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
                Current = parameters.GetValue<RoleDto>("Value");
                if (Current != null) GetDataById(Current.RoleId);
            }
        }

        List<UserDto> userTbDto = new List<UserDto>();
        private async void GetDataById(int id)
        {
            //待看  角色ID获取用户
            var result = await roleService.GetUsersByRoleIdAsync(id);
            if (result != null && result.Succeeded)
            {
                userTbDto.AddRange(result.Data);
            }
            var resultMenu = await service.GetAllAsync();//后期从xml文件加载
            if (resultMenu != null && resultMenu.Succeeded)
            {
                Users.Clear();
                foreach (var item in resultMenu.Data)
                {
                    if (userTbDto != null && userTbDto.Any(x => x.UserId == item.UserId))
                    {
                        item.IsSelected = true;
                    }
                    else
                    {
                        item.IsSelected = false;
                    }
                    Users.Add(item);
                }
            }
        }

        private async void Save()
        {
            List<UserDto> currentListUsers = new List<UserDto>();
            currentListUsers.AddRange(Users);
            List<UserRoleDto> AddList = new List<UserRoleDto>();
            List<UserRoleDto> UpdList = new List<UserRoleDto>();
            currentListUsers.ForEach(x =>
            {
                //删除数据
                if ((userTbDto.Exists(y => y.UserId == x.UserId && y.IsDel == 0) && !x.IsSelected))
                {
                    UpdList.Add(new UserRoleDto()
                    {
                        RoleId = Current.RoleId,
                        UserId = x.UserId,
                        State = 1,
                        CreateBy = "admin",
                        CreateDate = DateTime.Now
                    });
                }
                //设置逻辑删除后，删除的数据就无法查询出来了，这种方式不生效
                //if (userTbDto.Exists(y => y.UserId == x.UserId && y.IsDel == 1) && x.IsSelected)
                //{
                //    UpdList.Add(new UserRoleDto()
                //    {
                //        RoleId = Current.RoleId,
                //        UserId = x.UserId,
                //        State = 0,
                //        CreateBy = "admin",
                //        CreateDate = DateTime.Now
                //    });
                //}
                if (!userTbDto.Exists(y => y.UserId == x.UserId) && x.IsSelected)
                {
                    AddList.Add(new UserRoleDto()
                    {
                        RoleId = Current.RoleId,
                        UserId = x.UserId,
                        State = 0,
                        CreateBy ="admin",
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
