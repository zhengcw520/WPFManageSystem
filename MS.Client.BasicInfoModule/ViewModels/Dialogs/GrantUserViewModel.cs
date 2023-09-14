using HandyControl.Controls;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using MS.Client.Common;
using MySqlSugar.Shared;
using Newtonsoft.Json;
using MS.Client.Service;

namespace MS.Client.BasicInfoModule.ViewModels.Dialogs
{
    public class GrantUserViewModel : BindableBase, IDialogAware
    {
        public string Title { get; set; } = "分配用户";

        public event Action<IDialogResult> RequestClose;

        private ObservableCollection<UserTBDto> users;
        /// <summary>
        /// 在编辑画面显示的菜单
        /// </summary>
        public ObservableCollection<UserTBDto> Users
        {
            get { return users; }
            set { users = value; RaisePropertyChanged(); }
        }

        private RoleDto current;

        public RoleDto Current
        {
            get { return current; }
            set { current = value; RaisePropertyChanged(); }
        }

        private readonly IUserService service;
        private readonly IRoleService roleService;
        public GrantUserViewModel(IUserService _service, IRoleService _roleService)
        {
            Users = new ObservableCollection<UserTBDto>();
            service = _service;
            btnOKCommand = new DelegateCommand(Save);
            btnCancelCommand = new DelegateCommand(Cancel);
            roleService = _roleService;
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
            if (parameters.ContainsKey("Value"))
            {
                Current = parameters.GetValue<RoleDto>("Value");
                if (Current != null) GetDataById(Current.RoleId);
            }
        }

        List<UserRoleDto> userroleEntities = new List<UserRoleDto>();
        private async void GetDataById(int id)
        {
            //待看  角色ID获取用户
            var result = await roleService.GetUsersByRoleIdAsync(id);
            if (result != null && result.Succeeded)
            {
                //foreach (var item in result.Data)
                //{
                //    userroleEntities.Add(item);
                //}
            }
            var resultMenu = await service.GetAllAsync();//后期从xml文件加载
            if (resultMenu != null && resultMenu.Succeeded)
            {
                Users.Clear();
                foreach (var item in resultMenu.Data)
                {
                    if (userroleEntities != null && userroleEntities.Any(x => x.UserId == item.UserId && x.State == 0))
                    {
                        item.IsSelected = true;
                    }
                    else
                    {
                        item.IsSelected = false;
                    }
                    Users.Add(item);
                }
            }
        }

        private async void Save()
        {
            List<UserTBDto> currentListUsers = new List<UserTBDto>();
            currentListUsers.AddRange(Users);
            List<UserRoleDto> AddList = new List<UserRoleDto>();
            List<UserRoleDto> UpdList = new List<UserRoleDto>();
            currentListUsers.ForEach(x =>
            {
                //删除数据
                if ((userroleEntities.Exists(y => y.UserId == x.UserId && y.State == 0) && !x.IsSelected))
                {
                    UpdList.Add(new UserRoleDto()
                    {
                        RoleId = Current.RoleId,
                        UserId = x.UserId,
                        State = 1,
                        CreateBy = "admin",
                        CreateDate = DateTime.Now
                    });
                }
                if (userroleEntities.Exists(y => y.UserId == x.UserId && y.State == 1) && x.IsSelected)
                {
                    UpdList.Add(new UserRoleDto()
                    {
                        RoleId = Current.RoleId,
                        UserId = x.UserId,
                        State = 0,
                        CreateBy = "admin",
                        CreateDate = DateTime.Now
                    });
                }
                if (!userroleEntities.Exists(y => y.UserId == x.UserId) && x.IsSelected)
                {
                    AddList.Add(new UserRoleDto()
                    {
                        RoleId = Current.RoleId,
                        UserId = x.UserId,
                        State = 0,
                        CreateBy ="admin",
                        CreateDate = DateTime.Now
                    });
                }
            });
            UserBatchModel batchModel = new UserBatchModel() { AddModel = AddList, DelModel = UpdList, Model = null };
            var result = await service.BatchUserRolesAsync(batchModel);
            if (result != null && result.Succeeded)
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
