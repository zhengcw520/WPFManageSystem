using DryIoc;
using HandyControl.Controls;
using HandyControl.Data;
using ImTools;
using Prism.Commands;
using Prism.Common;
using Prism.Events;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows;
using MS.Client.BasicInfoModule.Views;
using MS.Client.Common;
using MS.Client.IService;
using MySqlSugar.Shared;
using MessageBox = HandyControl.Controls.MessageBox;

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
        private ObservableCollection<UserTBDto> users;
        public ObservableCollection<UserTBDto> Users
        {
            get { return users; }
            set { users = value; RaisePropertyChanged(); }
        }

        public DelegateCommand<UserTBDto> EditCommand { get; set; }
        #endregion

        #region 构造函数
        public UserManageViewModel(IDialogService _dialogService,IUserService _userService, 
            IRegionManager region, IEventAggregator _eventAggregator, IContainer container) 
            : base(region, container, _eventAggregator)
        {
            PageTitle = "用户列表";
            Users = new ObservableCollection<UserTBDto>();
            userService = _userService;
            dialogService = _dialogService;
            EditCommand = new DelegateCommand<UserTBDto>(Edit);
            PageCommond = new DelegateCommand<Pagination>(PageChange);
        }

        #endregion
        private void Edit(UserTBDto obj)
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
                        var user = callback.Parameters.GetValue<UserTBDto>("Value");
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
                        var user = callback.Parameters.GetValue<UserTBDto>("Value");
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
                var result = await userService.GetPageListAsync(findParameter);
                PageCount = Convert.ToInt32(result.Total) == 1 ? 1 : (int)Math.Ceiling(Convert.ToDouble(result.Total) / PageSize);
                if (result != null && result.Status)
                {
                    Users.Clear();
                    foreach (var item in result.Result)
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
    }
}
