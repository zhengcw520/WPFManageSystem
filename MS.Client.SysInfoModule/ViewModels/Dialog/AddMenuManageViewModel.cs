namespace MS.Client.SysInfoModule.ViewModels.Dialog
{
    public class AddMenuManageViewModel : BindableBase, IDialogAware
    {
        public string Title { get; set; } = "菜单";

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog()
        {
            return true;
        }

        private MenuDto current;

        public MenuDto Current
        {
            get { return current; }
            set { current = value; RaisePropertyChanged(); }
        }

        public void OnDialogClosed()
        {

        }

        public async void OnDialogOpened(IDialogParameters parameters)
        {
            if (parameters.ContainsKey("Value"))
            {
                var receiveData = parameters.GetValue<MenuDto>("Value");
                Title = (receiveData == null? "新增" : "修改") + Title;
                if (receiveData == null)
                {
                    MenuDto MenuDto = new MenuDto()
                    {
                        MenuName = string.Empty,
                        MenuIcon = string.Empty,
                        MenuPath = string.Empty,
                        ParentName = string.Empty,
                        CreateBy = "admin",
                        CreateDate = DateTime.Now
                    };
                    Current = MenuDto;
                }
                else
                {
                    if (receiveData == null) return;
                    GetMenuById(receiveData.MenuId);
                }
            }
        }

        private async void GetMenuById(int id)
        {
            var result = await menuService.GetFirstOfDefaultAsync(id);
            if (result != null && result.Succeeded)
            {
                Current = result.Data;
            }
        }

        public ObservableCollection<string> Icons { get; set; } = new ObservableCollection<string>();

        private readonly IMenuService menuService;
        public AddMenuManageViewModel(IMenuService _menuService,ILocalDataAccess localDataAccess)
        {
            menuService = _menuService;
            btnOKCommand = new DelegateCommand(Save);
            btnCancelCommand = new DelegateCommand(Cancel);

            // 初始化图标列表
            var icons = localDataAccess.GetIcons();
            for (int i = 0; i < icons.Count; i++)
            {
                Icons.Add(((char)int.Parse(icons[i], System.Globalization.NumberStyles.HexNumber)).ToString());
            }
        }

        private async void Save()
        {
            if (Current == null)
            {
                MessageBox.Show("当前数据为空不能保存!");
                return; 
            }
            var result = await menuService.AddOrUpdateAsync(Current);
            if (result != null && result.Succeeded)
            {
                DialogParameters para = new DialogParameters();
                para.Add("Value", Current);
                RequestClose?.Invoke(new DialogResult(ButtonResult.OK, para));
                MessageBox.Show("保存成功");
            }
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
