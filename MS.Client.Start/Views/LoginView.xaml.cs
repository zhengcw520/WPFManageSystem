using HandyControl.Controls;
using Prism.Events;
using System.Windows.Controls;
using System.Windows.Input;
using MS.Client.Common;
using MS.Client.Common.Messages;
using MS.Client.Service;
using MS.Client.Start.ViewModels;

namespace MS.Client.Start.Views
{
    /// <summary>
    /// LoginView.xaml 的交互逻辑
    /// </summary>
    public partial class LoginView :System.Windows.Window
    {
        public LoginView(//ILoginService loginService, IEventAggregator eventAggregator
            )
        {
            InitializeComponent();
            //this.DataContext = new LoginViewModel(loginService, eventAggregator);
            //FrameMessenger.Subscribe<SendMessageMsg>(eventAggregator,GetErrorInfo);
        }

        private void GetErrorInfo(SendMessageMsg obj)
        {
            //if (!string.IsNullOrEmpty(obj.SendMessage))
            //{
            //    //Growl.Error($"登录失败:{obj.SendMessage}", "ErrorMsg");
            //}
        }

        private void MinWin_click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void CloseWin_click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            { 
                this.DragMove();
            }
        }
         
        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
