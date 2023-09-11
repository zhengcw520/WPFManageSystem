using DryIoc;
using HandyControl.Controls;
using Prism.Commands;
using Prism.Events;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using MS.Client.IService;
using MySqlSugar.Shared;
using MS.Client.Common;

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
            PageCommond = new DelegateCommand<Pagination>(PageChange);
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
            if (result != null && result.Status)
            {
                FileList.Clear();
                foreach (var item in result.Result)
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

        private int pageIndex = 1;

        /// <summary>
        /// 第几页
        /// </summary>
        public int PageIndex
        {
            get { return pageIndex; }
            set { pageIndex = value; RaisePropertyChanged(); }
        }

        private int pageSize = 10;

        /// <summary>
        /// 每页最大条数
        /// </summary>
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value; RaisePropertyChanged(); }
        }


        public DelegateCommand FindCommand { get; set; }
        public DelegateCommand AddCommand { get; set; }
        public DelegateCommand<MenuDto> EditCommand { get; set; }

        public DelegateCommand<Pagination> PageCommond { get; set; }
    }
}
