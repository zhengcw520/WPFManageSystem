using DryIoc;
using ImTools;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MS.Client.Common;
using MS.Client.Service;
using MySqlSugar.Shared;
using Newtonsoft.Json;

namespace MS.Client.BasicInfoModule.ViewModels.Dialogs
{
    public class AddRoleViewModel : BindableBase, IDialogAware
    {
        public string Title { get; set; } = "角色";

        public event Action<IDialogResult> RequestClose;

        private RoleDto current;

        public RoleDto Current
        {
            get { return current; }
            set { current = value; RaisePropertyChanged(); }
        }

        private int type;

        /// <summary>
        /// 新增还是修改
        /// </summary>
        public int Type
        {
            get { return type; }
            set { type = value; RaisePropertyChanged(); }
        }

        private readonly IRoleService service;
        public AddRoleViewModel(IRoleService _service)
        {
            service = _service;
            btnOKCommand = new DelegateCommand(Save);
            btnCancelCommand = new DelegateCommand(Cancel);
        }

        public bool CanCloseDialog()
        {
            return true;
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
                RoleDto RoleDto = new RoleDto()
                {
                    RoleId = 0,
                    RoleName = string.Empty,
                    RoleDesc = string.Empty,
                    CreateBy = "admin",
                    CreateDate = DateTime.Now
                };
                Current = RoleDto;
            }
            else
            {
                if (parameters.ContainsKey("Data"))
                {
                    var receiveData = parameters.GetValue<RoleDto>("Data");
                    if (receiveData != null) GetDataById(receiveData.RoleId);
                }
            }
          // FindMenuInfo();
        }

        private async void GetDataById(int id)
        {
            var result = await service.GetFirstOfDefaultAsync(id);
            if (result != null && result.succeeded)
            {
                Current = (RoleDto)result.Result;
            }
        }

        private async void Save()
        {
            if (Current == null)
            {
                MessageBox.Show("当前数据为空不能保存!");
                return;
            }
            var result = await service.AddOrUpdateAsync(Current);
            if (result != null && result.succeeded)
                RequestClose?.Invoke(new DialogResult(ButtonResult.OK));
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
