using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SugarDemo.Shared
{
    /// <summary>
    /// 菜单
    /// </summary>
    public class MenuDto : BaseDto
    {
        private string? menuName;
        private string? menuPath;
        private string? parentName;
        private string? menuIcon;
        private bool isSelected;

        [SugarColumn(ColumnDescription = "菜单Id")]
        public int MenuId { get; set; }

        [SugarColumn(ColumnDescription = "菜单名称")]
        public string? MenuName
        {
            get { return menuName; }
            set { menuName = value; OnPropertyChanged(); }
        }

        [SugarColumn(ColumnDescription = "导航路径")]
        public string? MenuPath
        {
            get { return menuPath; }
            set { menuPath = value; OnPropertyChanged(); }
        }

        [SugarColumn(ColumnDescription = "父菜单")]
        public string? ParentName
        {
            get { return parentName; }
            set { parentName = value; OnPropertyChanged(); }
        }

        [SugarColumn(ColumnDescription = "菜单图标")]
        public string? MenuIcon
        {
            get { return menuIcon; }
            set { menuIcon = value; OnPropertyChanged(); }
        }

        [SugarColumn(ColumnDescription = "是否选中")]
        public bool IsSelected
        {
            get { return isSelected; }
            set { isSelected = value; OnPropertyChanged(); }
        }
    }
}
