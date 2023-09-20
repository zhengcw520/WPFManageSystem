using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SugarDemo.Shared
{
    /// <summary>
    /// 用户
    /// </summary>
    public class UserDto : BaseDto
    {
        private string userName;
        private string account;
        private string userIcon;
        private string address;
        private string telPhone;
        private string password;
        private int age;
        private int isEnable;
        private bool isSelected;
        private List<RoleDto>? roleDtoList;

        [SugarColumn(ColumnDescription = "用户Id")]
        public int UserId
        {
            get;
            set;
        }

        [SugarColumn(ColumnDescription = "用户名")]
        public string UserName
        {
            get { return userName; }
            set { userName = value; OnPropertyChanged(); }
        }

        [SugarColumn(ColumnDescription = "账号")]
        public string Account
        {
            get { return account; }
            set { account = value; OnPropertyChanged(); }
        }

        [SugarColumn(ColumnDescription = "图标")]
        public string UserIcon
        {
            get { return userIcon; }
            set { userIcon = value; OnPropertyChanged(); }
        }

        [SugarColumn(ColumnDescription = "地址")]
        public string Address
        {
            get { return address; }
            set { address = value; OnPropertyChanged(); }
        }

        [SugarColumn(ColumnDescription = "电话")]
        public string TelPhone
        {
            get { return telPhone; }
            set { telPhone = value; OnPropertyChanged(); }
        }

        [SugarColumn(ColumnDescription = "密码")]
        public string Password
        {
            get { return password; }
            set { password = value; OnPropertyChanged(); }
        }

        [SugarColumn(ColumnDescription = "年龄")]
        public int Age
        {
            get { return age; }
            set { age = value; OnPropertyChanged(); }
        }

        [SugarColumn(ColumnDescription = "是否启用")]
        public int IsEnable
        {
            get { return isEnable; }
            set { isEnable = value; OnPropertyChanged(); }
        }
  
        [SugarColumn(ColumnDescription = "是否选中")]
        public bool IsSelected
        {
            get { return isSelected; }
            set { isSelected = value; OnPropertyChanged(); }
        }

        public List<RoleDto>? RoleList 
        {
            get { return roleDtoList; }
            set { roleDtoList = value; OnPropertyChanged(); }
        }//只能是null不能赋默认值
    }
}
