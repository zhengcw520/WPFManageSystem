namespace MS.Client.BasicInfoModule.ViewModels
{
    public class UserManageViewModel:NavigationViewModel
    {
        #region 私有属性
        private readonly IUserService userService;
        private readonly IEventAggregator eventAggregator;
        private readonly IDialogService dialogService;
        #endregion

        #region 属性
        private ObservableCollection<UserDto> users;
        public ObservableCollection<UserDto> Users
        {
            get { return users; }
            set { users = value; RaisePropertyChanged(); }
        }

        public DelegateCommand<UserDto> EditCommand { get; set; }
        public DelegateCommand<UserDto> GrantRoleCommand { get; set; }
        #endregion

        #region 构造函数
        public UserManageViewModel(IDialogService _dialogService,IUserService _userService, 
            IRegionManager region, IEventAggregator _eventAggregator, IContainer container) 
            : base(region, container, _eventAggregator)
        {
            PageTitle = "用户列表";
            Users = new ObservableCollection<UserDto>();
            userService = _userService;
            dialogService = _dialogService;
            EditCommand = new DelegateCommand<UserDto>(Edit);
            PageCommond = new DelegateCommand<Pagination>(PageChange);
            GrantRoleCommand = new DelegateCommand<UserDto>(GrantRoleToUser);
        }

        #endregion
        private void Edit(UserDto obj)
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
                para.Add("Value", obj);
                dialogService.ShowDialog("AddUserView", para, callback =>
                {
                    if (callback != null && callback.Result == ButtonResult.OK)
                    {
                        var user = callback.Parameters.GetValue<UserDto>("Value");
                        if (user != null)
                        {
                            var result = Users.FirstOrDefault(x => x.UserId == user.UserId);
                            if (result != null) 
                            {  
                                result.UserIcon = user.UserIcon;    
                                result.Address = user.Address;    
                                result.TelPhone = user.TelPhone;    
                                result.Age = user.Age;      
                            }
                        }
                        MessageBox.Show("保存成功");
                    }
                });
            }
            catch(Exception ex)
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
                dialogService.ShowDialog("AddUserView", para, callback =>
                {
                    if (callback != null && callback.Result == ButtonResult.OK)
                    {
                        var user = callback.Parameters.GetValue<UserDto>("Value");
                        if (user != null)
                        {
                            Users.Add(user);    
                        }
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
                var res = await userService.GetPageListAsync(findParameter);
                if (res != null && res.Succeeded)
                {
                    PageCount = Convert.ToInt32(res.Data!.TotalCount) == 1 ? 1 : (int)Math.Ceiling(Convert.ToDouble(res.Data!.TotalCount) / PageSize);
                    Users.Clear();
                    foreach (var item in res.Data.Items)
                    {
                        Users.Add(item);
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

        /// <summary>
        /// 给用户分配角色
        /// </summary>
        /// <param name="obj"></param>
        private void GrantRoleToUser(UserDto obj)
        {
            try
            {
                DialogParameters para = new DialogParameters();
                para.Add("Value", obj);
                dialogService.ShowDialog("GrantRoleView", para, callback =>
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
    }
}
