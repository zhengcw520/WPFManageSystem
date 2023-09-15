namespace SugarDemo.Core
{
    [SugarTable(tableName: "RoleTB")]
    public class RoleTB : BaseTable
    {
        [SugarColumn(IsIdentity = true, IsPrimaryKey = true)]
        public int RoleId { get; set; }

        [SugarColumn(ColumnDescription = "角色名称", Length = 20)]
        public string RoleName { get; set; }

        [SugarColumn(ColumnDescription = "角色描述", Length = 100, IsNullable = true)]
        public string RoleDesc { get; set; }

        [Navigate(typeof(UserRoleTB), nameof(UserRoleTB.RoleId), nameof(UserRoleTB.UserId))]//注意顺序
        public List<UserTB> UserList { get; set; }//只能是null不能赋默认值

        [Navigate(typeof(RoleMenuTB), nameof(RoleMenuTB.RoleId), nameof(RoleMenuTB.MenuId))]//注意顺序
        public List<MenuTB> MenuList { get; set; }//只能是null不能赋默认值
    }
}
