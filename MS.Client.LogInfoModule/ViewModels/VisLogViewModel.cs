namespace MS.Client.LogInfoModule.ViewModels
{
    public class VisLogViewModel : NavigationViewModel
    {
        #region 属性
        private readonly IVisLogService _service;

        private ObservableCollection<VisLogDto> logs;
        public ObservableCollection<VisLogDto> Logs
        {
            get { return logs; }
            set { logs = value; RaisePropertyChanged(); }
        }
        #endregion
        public VisLogViewModel(IRegionManager regionManager, IContainer container, IEventAggregator aggregator, IVisLogService service) 
            :base(regionManager, container, aggregator)
        {
            PageTitle = "访问日志";
            Logs = new ObservableCollection<VisLogDto>();
            _service = service;
        }

        protected override async void Find()
        {
            try
            {
                ShowLoading();
                FindParameter findParameter = new FindParameter() { PageIndex = PageIndex, PageSize = PageSize, Search = "" };
                var res = await _service.GetPageListAsync(findParameter);
                if (res != null && res.Succeeded)
                {
                    PageCount = Convert.ToInt32(res.Data!.TotalCount) == 1 ? 1 : (int)Math.Ceiling(Convert.ToDouble(res.Data!.TotalCount) / PageSize);
                    Logs.Clear();
                    foreach (var item in res.Data.Items)
                    {
                        Logs.Add(item);
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
    }
}
