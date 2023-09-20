namespace MS.Client.LogInfoModule.ViewModels
{
    public class OpLogViewModel : NavigationViewModel
    {
        #region 属性
        private readonly IOpLogService _service;

        private ObservableCollection<OpLogDto> logs;
        public ObservableCollection<OpLogDto> Logs
        {
            get { return logs; }
            set { logs = value; RaisePropertyChanged(); }
        }
        #endregion
        public OpLogViewModel(IRegionManager regionManager, IContainer container, IEventAggregator aggregator, IOpLogService service) 
            : base(regionManager, container, aggregator)
        {
            PageTitle = "操作日志";
            Logs = new ObservableCollection<OpLogDto>();
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
