using Furion.RemoteRequest.Extensions;
using MySqlSugar.Shared;
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

namespace WPFTestApi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var model = GetAllAsync();
            if (model != null)
            { 
            
            }
        }

        public async Task<FurApiResponse<List<MenuDto>>> GetAllAsync()
        {
            //https://localhost:44342/api/menu/all
            return await "api/menu/all".SetClient("github").GetAsAsync<FurApiResponse<List<MenuDto>>>();
        }
    }
}
