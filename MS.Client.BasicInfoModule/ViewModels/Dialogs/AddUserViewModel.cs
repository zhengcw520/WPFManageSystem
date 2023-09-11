using HandyControl.Controls;
using HandyControl.Data;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MS.Client.Common;
using MySqlSugar.Shared;
using MS.Client.IService;

namespace MS.Client.BasicInfoModule.ViewModels
{
    public class AddUserViewModel:BindableBase, IDialogAware
    {
        public string Title { get; set; } = "用户";

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog()
        {
            return true;
        }

        private UserTBDto current;

        public UserTBDto Current
        {
            get { return current; }
            set { current = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<RoleDto> roles;

        public ObservableCollection<RoleDto>  Roles
        {
            get { return roles; }
            set { roles = value; RaisePropertyChanged(); }
        }

        private int type;

        public int Type
        {
            get { return type; }
            set { type = value; RaisePropertyChanged(); }
        }

        public void OnDialogClosed()
        {

        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            Type = (int)OperTypeEnum.Add;
            if (parameters.ContainsKey("OperType"))
            {
                Type = parameters.GetValue<int>("OperType");
                Title = (Type == (int)OperTypeEnum.Add ? "新增" : "修改") + Title;
            }

            if (type == (int)OperTypeEnum.Add)
            {
                UserTBDto UserTBDto = new UserTBDto()
                {
                    UserId = 0,
                    UserName = string.Empty,
                    UserIcon = string.Empty,
                    Address = string.Empty,
                    TelPhone = string.Empty,
                    Age = 20,
                    Account = string.Empty,
                    CreateBy = "admin",
                    CreateDate = DateTime.Now
                };
                Current = UserTBDto;
            }
            else
            {
                if (parameters.ContainsKey("Value"))
                {
                    var receiveData = parameters.GetValue<UserTBDto>("Value");
                    if (receiveData != null) GetDataById(receiveData.UserId);
                }
            }
        }

        private async void GetDataById(int id)
        {
            var result = await service.GetFirstOfDefaultAsync(id);
            if (result != null && result.Status)
            {
                Current = result.Result;
            }
            var roleResult = await roleservice.GetAllAsync();
            if (roleResult != null && roleResult.Status)
            {
                //Roles.AddRange(roleResult.Result);
            }
        }

        private List<int> cataList;

        public List<int> CataList
        {
            get { return cataList; }
            set { cataList = value; RaisePropertyChanged(); }
        }

        private readonly IUserService service;
        private readonly IRoleService roleservice;
        public AddUserViewModel(IUserService _service,IRoleService _roleservice)
        {
            Roles = new ObservableCollection<RoleDto>();
            service = _service;
            roleservice = _roleservice; 
            btnOKCommand = new DelegateCommand(Save);
            btnCancelCommand = new DelegateCommand(Cancel);
        }

        private async void Save()
        {
            if (Current == null)
            {
                NotifyIcon.ShowBalloonTip("", "当前数据为空不能保存!", NotifyIconInfoType.Warning, "NotifyIconToken");
                return;
            }
            var result = await service.AddOrUpdateAsync(Current);
            if (result != null && result.Status)
            {
                DialogParameters para = new DialogParameters();
                para.Add("Value", Current);
                RequestClose?.Invoke(new DialogResult(ButtonResult.OK,para));
            }
            else
            {
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
