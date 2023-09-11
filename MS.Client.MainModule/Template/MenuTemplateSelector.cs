using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using MS.Client.MainModule.Models;

namespace MS.Client.MainModule.Template
{
    public class MenuTemplateSelector : DataTemplateSelector
    {
        public DataTemplate GroupTemplate { get; set; }
        public DataTemplate ExpanderTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            ModuleGroup group = (ModuleGroup)item;
            if (group != null)
            {
                if (!group.ContractionTemplate)
                    return ExpanderTemplate;
                else
                    return GroupTemplate;
            }
            return ExpanderTemplate;
        }
    }
}
