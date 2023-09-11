using HandyControl.Controls;
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
using MS.Client.MainModule.Models;

namespace MS.Client.MainModule.Views
{
    /// <summary>
    /// TreeMenuView.xaml 的交互逻辑
    /// </summary>
    public partial class TreeMenuView : UserControl
    {
        public TreeMenuView()
        {
            InitializeComponent();
        }

        private void SearchBar_OnSearchStarted(object sender, HandyControl.Data.FunctionEventArgs<string> e)
        {
            //string keyword = e.Info;
            //List<MenuItemModel> results = new List<MenuItemModel>();
            //foreach (TreeViewItem item in MenuTree.Items)
            //{
            //    FindMatchs(keyword,item,ref results);
            //}
            //MenuTree.ItemsSource = results;
        }

        private void FindMatchs(string keyword, TreeViewItem node, ref List<MainMenuItemModel> results)
        {
            //if (node.Header.ToString().Contains(keyword))
            //{
            //    results.Add(node,sou);
            //}
        }
    }
}
