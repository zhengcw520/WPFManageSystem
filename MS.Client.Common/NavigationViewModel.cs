using DryIoc;
using HandyControl.Controls;
using Prism.Commands;
using Prism.Events;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using MS.Client.Common.Messages;

namespace MS.Client.Common
{
    public class NavigationViewModel : BindableBase, INavigationAware
    {
        private readonly IContainerProvider containerProvider;
        public readonly IEventAggregator aggregator;

        public NavigationViewModel(IRegionManager regionManager, IContainer container, IEventAggregator aggregator)
        {
            this.aggregator = aggregator;
            ExcuteCommand = new DelegateCommand<string>(Execute);
            PageCommond = new DelegateCommand<Pagination>(PageChange);
            TabCloseCommand = new DelegateCommand<string>((param) =>
            {
                //var registrations = container.GetServiceRegistrations();
                //foreach (var registration in registrations)
                //{
                //    Console.WriteLine("ServiceType: {0}, ImplementationType: {1}, Reuse: {2}",
                //        registration.ServiceType.FullName, registration.ImplementationType.FullName, registration.ServiceType.Namespace);
                //}

                var obj = container.GetServiceRegistrations().Where(v => Convert.ToString(v.OptionalServiceKey) == NavUri).FirstOrDefault();
                string name = obj.ImplementationType.Name;
                if (!string.IsNullOrEmpty(name))
                {
                    var region = regionManager.Regions["MainContentRegion"];
                    //foreach (var item in region.Views)
                    //{
                    //    Console.WriteLine("{0},{1},{2},{3}",item.GetType(),item.GetType().Name, item.GetType().FullName,item.GetType().Namespace);
                    //}
                    var view = region.Views.Where(v => v.GetType().Name == name).FirstOrDefault();
                    if (view != null)
                        region.Remove(view);
                }
            });
        }

        protected void ShowLoading(string tip = "正在加载....")
        {
            aggregator?.GetEvent<LoadingEvent>().Publish(new LoadingPayload { IsShow = true, Message = tip });
        }
        protected void HideLoading()
        {
            aggregator?.GetEvent<LoadingEvent>().Publish(new LoadingPayload { IsShow = false });
        }

        public virtual bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public virtual void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public Action<NavigationContext> NavigatedToAction;
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            NavUri = navigationContext.Uri.ToString();
            // 传递过来的车道
            NavigatedToAction?.Invoke(navigationContext);
            InitFind();
        }

        protected virtual void PageChange(Pagination obj)
        {
            PageIndex = obj.PageIndex;
            Find();
        }

        protected virtual void InitFind() 
        {
        
        }


        private void Execute(string obj)
        {
            switch (obj)
            {
                case "新增": Add(); break;
                case "查询": Find(); break;
                case "保存": Save(); break;
            }
        }

        protected virtual void Add()
        { 
        
        }

        protected virtual void Find()
        {

        }

        protected virtual void Save()
        {

        }

        public void UpdateLoading(bool IsOpen)
        {
            //aggregator.UpdateLoading(new Common.Events.UpdateModel()
            //{
            //    IsOpen = IsOpen
            //});
        }

        private int pageIndex = 1;

        /// <summary>
        /// 第几页
        /// </summary>
        public int PageIndex
        {
            get { return pageIndex; }
            set { pageIndex = value; RaisePropertyChanged(); }
        }

        private int pageSize = 7;

        /// <summary>
        /// 每页最大条数
        /// </summary>
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value; RaisePropertyChanged(); }
        }

        private int pageCount;

        /// <summary>
        /// 页数
        /// </summary>
        public int PageCount
        {
            get { return pageCount; }
            set { pageCount = value; RaisePropertyChanged(); }
        }

        public DelegateCommand<string> ExcuteCommand { get; set; }

        //public DelegateCommand<object> EditCommand { get; set; }
        public DelegateCommand<Pagination> PageCommond { get; set; }
        public DelegateCommand<string> TabCloseCommand { get; private set; }

        public string PageTitle { get; set; } = "默认tab";

        public bool IsCanClose { get; set; } = true;
        public string NavUri { get; set; }
    }
}
