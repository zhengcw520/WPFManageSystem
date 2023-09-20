namespace MS.Client.SysInfoModule.ViewModels
{
    public class FileUploadViewModel : NavigationViewModel
    {
        //在app.xmal中注册，不然报错WPF Unable to resolve resolution root FileUploadViewModel
        private readonly IFileUpgradeService fileUpgradeService;
        public FileUploadViewModel(IFileUpgradeService fileUpgradeService, IEventAggregator aggregator, IRegionManager regionManager,IContainer container):
            base(regionManager,container,aggregator)
        {
            PageTitle = "文件上传列表";
            FileList = new ObservableCollection<UpgradeFileDto>();
            this.fileUpgradeService = fileUpgradeService;
            FindCommand = new DelegateCommand(Find);
            AddCommand = new DelegateCommand(Add);
            EditCommand = new DelegateCommand<MenuDto>(Edit);
        }

        private void Edit(MenuDto obj)
        {
            //HandyControl.Controls.Dialog.Show(new AddUserView());
            //dialogHost.ShowDialog("AddUserView");

        }

        protected override void Add()
        {
            //DialogParameters param = new DialogParameters();
            //dialogHost.Show("AddUserView", param, x => { 

            //});
            //HandyControl.Controls.Dialog.Show(new AddUserView());

            //MenuDto MenuDto = new MenuDto() { UserName="mis02",Account="mis02", Password="123456", RealName="王二狗",UserIcon=""  };
            //var result = userBll.AddUser(MenuDto);
            //if (result != null)
            //{ 

            //}
        }

        protected override async void Find()
        {
            FindParameter findParameter = new FindParameter() { PageIndex = PageIndex - 1, PageSize = PageSize };
            var result = await fileUpgradeService.GetPageListAsync(findParameter);
            if (result != null && result.Succeeded)
            {
                var model = result.Data;
                FileList.Clear();
                foreach (var item in model.Items)
                {
                    FileList.Add(item);
                }
            }
        }

        private ObservableCollection<UpgradeFileDto> fileList;

        public ObservableCollection<UpgradeFileDto> FileList
        {
            get { return fileList; }
            set { fileList = value; RaisePropertyChanged(); }
        }

        public DelegateCommand FindCommand { get; set; }
        public DelegateCommand AddCommand { get; set; }
        public DelegateCommand<MenuDto> EditCommand { get; set; }
    }
}
