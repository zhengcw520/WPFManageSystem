using DryIoc;
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
using MS.Client.Common;
using MS.Client.IService;
using MySqlSugar.Shared;
using Newtonsoft.Json;

namespace MS.Client.BasicInfoModule.ViewModels.Dialogs
{
    public class GrantMenuViewModel : BindableBase, IDialogAware
    {
        public string Title { get; set; } = "分配菜单";

        public event Action<IDialogResult> RequestClose;

        private ObservableCollection<MenuDto> menus;
        /// <summary>
        /// 在编辑画面显示的菜单
        /// </summary>
        public ObservableCollection<MenuDto> Menus
        {
            get { return menus; }
            set { menus = value; RaisePropertyChanged(); }
        }

        private RoleDto current;

        public RoleDto Current
        {
            get { return current; }
            set { current = value;RaisePropertyChanged(); }
        }


        private readonly IRoleService service;
        private readonly IMenuService menuService;
        public GrantMenuViewModel(IRoleService _service, IMenuService _menuService)
        {
            Menus = new ObservableCollection<MenuDto>();
            service = _service;
            menuService = _menuService;
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
            if (parameters.ContainsKey("Value"))
            {
                Current = parameters.GetValue<RoleDto>("Value");
                if (Current != null) GetDataById(Current.RoleId);
            }
        }

        List<RoleMenuDto> roleMenuEntities = new List<RoleMenuDto>();
        private async void GetDataById(int id)
        {
            var rolemenu = await service.GetMenusByRoleIdAsync(id);
            if (rolemenu != null && rolemenu.Status)
            {
                roleMenuEntities = JsonConvert.DeserializeObject<List<RoleMenuDto>>(rolemenu.Result.ToString());
            }
            var resultMenu = await menuService.GetAllAsync();//后期从xml文件加载
            if (resultMenu != null && resultMenu.Status)
            {   
                Menus.Clear();
                foreach (var item in resultMenu.Result)
                {
                    if (roleMenuEntities != null && roleMenuEntities.Any(x => x.MenuId == item.MenuId && x.State == 0))
                    {
                        item.IsSelected = true;
                    }
                    else
                    {
                        item.IsSelected = false;
                    }
                    Menus.Add(item);
                }  
            }
        }

        private async void Save()
        {
            List<MenuDto> currentListMenus = new List<MenuDto>();
            currentListMenus.AddRange(Menus);
            List<RoleMenuDto> AddList = new List<RoleMenuDto>();
            List<RoleMenuDto> UpdList = new List<RoleMenuDto>();
            currentListMenus.ForEach(x =>
            {
                //删除数据
                if ((roleMenuEntities.Exists(y => y.MenuId == x.MenuId && y.State ==0) && !x.IsSelected))
                {
                    UpdList.Add(new RoleMenuDto()
                    {
                        RoleId = Current.RoleId,
                        MenuId = x.MenuId,
                        State = 1
                    });
                }
                if(roleMenuEntities.Exists(y => y.MenuId == x.MenuId && y.State == 1) && x.IsSelected)
                {
                    UpdList.Add(new RoleMenuDto()
                    {
                        RoleId = Current.RoleId,
                        MenuId = x.MenuId,
                        State = 0
                    });
                }
                if (!roleMenuEntities.Exists(y => y.MenuId == x.MenuId) && x.IsSelected)
                {
                    AddList.Add(new RoleMenuDto()
                    {
                        RoleId = Current.RoleId,
                        MenuId = x.MenuId,
                        State = 0
                    });
                }
            });
            RoleBatchModel batchModel = new RoleBatchModel() { AddModel = AddList, DelModel = UpdList, Model = null };
            var result = await service.BatchUpdateRoleInfoAsync(batchModel);
            if (result != null && result.Status)
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
