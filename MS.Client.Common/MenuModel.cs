using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Client.Common
{
    public class MenuModel
    {
        public int MenuId { get; set; }
        public string? MenuIcon
        {
            get;
            set;
        }

        public string? MenuHeader
        {
            get;set;    
        }

        public string? TargetView
        {
            get; set;
        }

        public int Level
        {
            get; set;
        }

        public string? AuthorityType
        {
            get; set;
        }

        public int State
        {
            get; set;
        }

        public int ParentId
        {
            get; set;
        }

        public ObservableCollection<MenuModel> Children
        {
            get;set;
        }

        public Boolean IsChecked
        {
            get; set;
        }
    }
}
