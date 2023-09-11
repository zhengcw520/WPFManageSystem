using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MS.Client.Common.Messages;
using MS.Client.Common;
using Prism.Events;

namespace MS.Client.Start.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow :Window
    {
        public string PageTitle { get; set; } = "默认主";
        public MainWindow(//IEventAggregator eventAggregator
            )
        {
            InitializeComponent();
            //FrameMessenger.Subscribe<MessageModel>(eventAggregator, arg =>
            //{
            //    if (this.menu.Width < 200)
            //    {
            //        this.userName.Visibility = Visibility.Visible;
            //        AnimationHelper.CreateWidthChangedAnimation(this.menu, 50, 200, new TimeSpan(0, 0, 0, 0, 300));
            //    }
            //    else
            //    {
            //        this.userName.Visibility = Visibility.Collapsed;
            //        AnimationHelper.CreateWidthChangedAnimation(this.menu, 200, 50, new TimeSpan(0, 0, 0, 0, 300));
            //    }
            //    //由于...
            //    //var template = this.IC.ItemTemplateSelector;
            //    //this.IC.ItemTemplateSelector = null;
            //    //this.IC.ItemTemplateSelector = template;
            //},
            //filter => filter.Type.Equals("ExpandMenu"));
        }

        private void MinWin_click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void MaxWin_click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
            }
            else
            {
                this.WindowState = WindowState.Maximized;
                this.MaxHeight = SystemParameters.WorkArea.Height;//最大化后不遮掉任务栏
            }
        }

        private void CloseWin_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.menu.Width < 180)
            {
                this.userName.Visibility = Visibility.Visible;
                AnimationHelper.CreateWidthChangedAnimation(this.menu, 0, 180, new TimeSpan(0, 0, 0, 0, 300));
            }
            else
            {
                this.userName.Visibility = Visibility.Collapsed;
                AnimationHelper.CreateWidthChangedAnimation(this.menu, 180, 0, new TimeSpan(0, 0, 0, 0, 300));
            }
        }
    }
}
