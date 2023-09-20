namespace MS.Client.Start
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        private string PathUrl = string.Empty;
        public App()
        {
            Serve.RunNative(services =>
            {
                services.AddRemoteRequest(options =>
                {
                    // 配置 Github 基本信息
                    options.AddHttpClient("github", c =>
                    {
                        c.BaseAddress = new Uri(PathUrl);
                        c.DefaultRequestHeaders.Add("Authorization", $"Bearer {GlobalEntity.JwtToken}");
                        //c.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory-Sample");
                    });
                });
            });
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            // 获取应用程序的启动路径
            string startupPath = AppDomain.CurrentDomain.BaseDirectory;
            UserConfigHelper.Init(startupPath);
            ServiceHelper.Init(startupPath);
            PathUrl = ServiceHelper.ServicesInfo.FirstOrDefault().Value.ToString();
            base.OnStartup(e);
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void InitializeShell(Window shell)
        {
            if (Container.Resolve<LoginView>().ShowDialog() == false)
            {
                Application.Current?.Shutdown();
            }
            else
            {
                base.InitializeShell(shell);
            }
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<ILoginService,LoginService>();
            containerRegistry.Register<IUserService, UserService>();
            containerRegistry.Register<IMenuService, MenuService>();
            containerRegistry.Register<IRoleService, RoleService>();
            containerRegistry.Register<IVisLogService, VisLogService>();
            containerRegistry.Register<IOpLogService, OpLogService>();
            //containerRegistry.Register<IFileUpgradeService,FileUpgradeService>();
            containerRegistry.Register<ILocalDataAccess,LocalDataAccess>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            base.ConfigureModuleCatalog(moduleCatalog);
            // 自动扫描目录

            // 手动添加
            moduleCatalog.AddModule<MainModule.MainModule>();
            moduleCatalog.AddModule<BasicInfoModule.BasicInfoModule>();
            moduleCatalog.AddModule<SysInfoModule.SysInfoModule>();
            moduleCatalog.AddModule<LogInfoModule.LogInfoModule>();
        }
    }
}
