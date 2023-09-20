namespace MS.Client.Start.ViewModels
{
    public class MainWindowViewModel:BindableBase
    {
        private readonly IEventAggregator ea;
        public MainWindowViewModel(IEventAggregator _ea,RegionManager regionManager)
        {
            ea = _ea;
            //ExpandMenuCmd = new DelegateCommand(ExpandMenu);
            DoubleMenuCommand = new DelegateCommand<object>((arg) => 
            {
                regionManager.RequestNavigate("MainContentRegion", arg.ToString());
            });
            ea.GetEvent<LoadingEvent>().Subscribe(OnShowLoading, ThreadOption.UIThread);
        }

        private void OnShowLoading(LoadingPayload msg)
        {
            this.IsLoading = msg.IsShow;
            this.LoadingMessage = msg.Message;
        }

        private void ExpandMenu()
        {
            //MessageModel messageModel = new MessageModel()
            //{
            //    Type = "ExpandMenu",
            //    Message = "收缩按钮"
            //};
            //ea.Publish(messageModel);
        }

        private bool isLoading = false;

		public bool IsLoading
        {
			get { return isLoading; }
			set { isLoading = value; RaisePropertyChanged(); }
		}

        private string loadingMessage;

        public string LoadingMessage
        {
            get { return loadingMessage; }
            set { loadingMessage = value;RaisePropertyChanged(); }
        }

        public DelegateCommand ExpandMenuCmd { get; set; }   
        public DelegateCommand<object> DoubleMenuCommand{get; set; }    
        public DelegateCommand CancelLoadingCommand { get => new DelegateCommand(() => IsLoading = false); }
    }
}
