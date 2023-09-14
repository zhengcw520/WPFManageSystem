using Newtonsoft.Json;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MS.Client.Common.Messages;
using MS.Client.Common;
using Prism.Events;
using Prism.Mvvm;
using Prism.Commands;
using MS.Client.Service;
using System.Diagnostics;
using MySqlSugar.Shared;
using System.Configuration;
using System.Collections.ObjectModel;
using System.Data.SqlTypes;
using Furion.RemoteRequest.Extensions;
using ImTools;

namespace MS.Client.Start.ViewModels
{
    public class LoginViewModel : BindableBase
    {
        #region 属性
        private string account = "admin";

        public string Account 
        {
            get { return account; }
            set { account = value; RaisePropertyChanged(); }
        }

        private string password="1111";

        public string Password
        {
            get { return password; }
            set { password = value; RaisePropertyChanged(); }
        }

        private bool _isLoading;

        public bool IsLoading
        {
            get { return _isLoading; }
            set { _isLoading = value;RaisePropertyChanged(); }
        }

        private string _loadingMessage;

        public string LoadingMessage
        {
            get { return _loadingMessage; }
            set { _loadingMessage = value;RaisePropertyChanged(); }
        }

        public DelegateCommand<object> LoginCommand { get; set; }

        private Boolean isRemember;

        public Boolean IsRemember
        {
            get { return isRemember; }
            set { isRemember = value; RaisePropertyChanged(); }
        }

        #endregion

        #region 变量
        private readonly ILoginService _loginService;
        private readonly IUserService _userService;
        private readonly IEventAggregator _eventAggregator;
        private string TempPassword = string.Empty;
        #endregion

        #region 构造函数
        public LoginViewModel(ILoginService loginService, IUserService userService, IEventAggregator eventAggregator)
        {
            //string upgradePath = "D:\\Pror\\Owner\\myproject\\gitproject\\MS\\Client\\MS.Client.Upgrade\\bin\\Debug\\net7.0-windows\\MS.Client.Upgrade.exe";
            //// 需要更新的时候，启动更新程序，关闭主程序
            //Process process = Process.Start(
            //    upgradePath
            //    );
            //process.WaitForInputIdle();
            LoginCommand = new DelegateCommand<object>(Login);
            InitLoginInfo();
            _loginService = loginService;
            _userService = userService;
            _eventAggregator = eventAggregator;
        }
        #endregion

        #region 加载登录信息
        private void InitLoginInfo()
        {
            //string language = UserConfigHelper.Language;
            //string skinName = UserConfigHelper.SkinName;


            //加载用户配置文件
            if (UserConfigHelper.LoginHis == null)
            {
                return;
            }

            if (!UserConfigHelper.LoginHis.Keys.Contains(UserConfigHelper.LastLogin))
            {
                return;
            }

            UserConfigHelper.LoginUserInfo lastLogin =
                UserConfigHelper.LoginHis[UserConfigHelper.LastLogin];

            if (lastLogin == null)
            {
                return;
            }

            Account = lastLogin.LoginName;
            if (!string.IsNullOrEmpty(lastLogin.Pwd))
            {
                TempPassword = lastLogin.Pwd;
                Password = lastLogin.Pwd;
                IsRemember = true;
            }
        }
        #endregion

        #region 保存登录信息
        private void SaveUserConfig()
        {
            string loginName = Account;
            string pwd = string.IsNullOrEmpty(TempPassword)? TempPassword:DESHelper.GetMD5Str(Password);
            try
            {
                UserConfigHelper.LoginUserInfo loginInfo = null;
                if (UserConfigHelper.LoginHis == null)
                {
                    UserConfigHelper.LoginHis = new Dictionary<string, UserConfigHelper.LoginUserInfo>();
                }
                if (UserConfigHelper.LoginHis.Keys.Contains(loginName))
                {
                    loginInfo = UserConfigHelper.LoginHis[loginName];
                }
                if (loginInfo == null)
                {
                    loginInfo = new UserConfigHelper.LoginUserInfo();
                    loginInfo.LoginName = loginName;
                    if (IsRemember)
                    {
                        loginInfo.Pwd = pwd;
                        //loginInfo.Sign = signIn;
                    }
                    else
                    {
                        loginInfo.Sign = false;
                    }
                    UserConfigHelper.LoginHis.Add(loginInfo.LoginName,loginInfo);
                }
                else
                {
                    if (IsRemember)
                    {
                        loginInfo.Pwd = pwd;
                        //loginInfo.Sign = signIn;
                    }
                    else
                    {
                        loginInfo.Sign = false;
                    }
                }
                UserConfigHelper.LastLogin = loginName;
                UserConfigHelper.Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 登录按钮
        private async void Login(object obj)
        {
            if (string.IsNullOrEmpty(Account))
            {
                _eventAggregator.Publish(new SendMessageMsg() { SendMessage = "用户名为空" });
                return;
            }
            if (string.IsNullOrEmpty(Password))
            {
                _eventAggregator.Publish(new SendMessageMsg() { SendMessage = "密码为空" });
                return;
            }
            this.IsLoading = true;
            this.LoadingMessage = "正在登录";
            // 字典方式

            // 将加号替换为"%2B"
            //string urlEncodedPassword = !string.IsNullOrEmpty(TempPassword)? TempPassword: DESHelper.GetMD5Str(Password).Replace("+", "%2B");
            //string pas = DESHelper.GetMD5Str("1111");

//            var hh = await "api/login/login".SetClient("github").SetQueries(new Dictionary<string, string> {
//    { "Account", Account },
//    { "Password", Password}
//}).GetAsync();
            //var user = await "api/user/all".SetClient("github").GetAsStringAsync();

            var loginInput = new LoginInput() { Account = Account, Password = Password };
            var userResult = await _loginService.Login(loginInput);
            if (userResult != null)
            {
                GlobalEntity.JwtToken = userResult!.Data.JwtToken;
                var menuResult = await _userService.GetMenusByUserIdAsync(userResult!.Data.UserId);
                if (menuResult!=null)
                {
                    List<MenuDto> menuList = new List<MenuDto>();
                    foreach (var item in menuResult.Data)
                    {
                        menuList.Add(item);
                    }
                    GlobalEntity.MenuDtos = menuList;
                }
                SaveUserConfig();
                (obj as Window)!.DialogResult = GlobalEntity.MenuDtos != null;
            }
            else
            {
                _eventAggregator.Publish(new SendMessageMsg() { SendMessage = "登录失败,请联系管理员" });
                this.IsLoading = false;
            }
        }
        #endregion
    }
}
