using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Client.MainModule.ViewModels
{
    public class HomeViewModel:BindableBase
    {
        public string PageTitle { get; set; } = "主页";

        public HomeViewModel()
        {
            
        }
    }
}
