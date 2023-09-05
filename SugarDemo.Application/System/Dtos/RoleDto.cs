﻿namespace SugarDemo.Application
{
    /// <summary>
    /// 角色
    /// </summary>
    public class RoleDto : BaseDto
    {
        private string roleName;
        private string roleDesc;
        private List<UserTBDto> userList;
        private List<MenuDto> menuList;

        [SugarColumn(ColumnDescription = "角色Id")]
        public int RoleId { get; set; }

        [SugarColumn(ColumnDescription = "角色名称")]
        public string RoleName
        {
            get { return roleName; }
            set { roleName = value; OnPropertyChanged(); }
        }

        [SugarColumn(ColumnDescription = "角色描述")]
        public string RoleDesc
        {
            get { return roleDesc; }
            set { roleDesc = value; OnPropertyChanged(); }
        }
        public List<UserTBDto> UserDtoList
        {
            get { return userList; }
            set { userList = value; OnPropertyChanged(); }
        }

        public List<MenuDto> MenuDtoList
        {
            get { return menuList; }
            set { menuList = value; OnPropertyChanged(); }
        }
    }
}