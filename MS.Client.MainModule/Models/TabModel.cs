using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Client.MainModule.Models
{
    public class TabModel : BindableBase
    {
		private string header;

		public string Header
        {
			get { return header; }
			set { header = value; RaisePropertyChanged(); }
		}

        private string viewName;

        public string ViewName
        {
            get { return viewName; }
            set { viewName = value; RaisePropertyChanged(); }
        }

        private object content;

        public object Content
        {
            get { return content; }
            set { content = value; RaisePropertyChanged(); }
        }
    }
}
