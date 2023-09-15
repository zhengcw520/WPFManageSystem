namespace SugarDemo.Core
{
    [SugarTable(tableName: "MenuTB")]
    public class MenuTB : BaseTable
    {
        [SugarColumn(IsIdentity = true, IsPrimaryKey = true)]
        public int MenuId { get; set; }

        [SugarColumn(ColumnDescription = "名称", Length = 20)]
        public string MenuName { get; set; }

        [SugarColumn(ColumnDescription = "路径", Length = 20,IsNullable =true)]
        public string MenuPath { get; set; }

        [SugarColumn(ColumnDescription = "图标", Length = 10, IsNullable = true)]
        public string MenuIcon { get; set; }

        [SugarColumn(ColumnDescription = "父菜单", Length = 20, IsNullable = true)]
        public string ParentName { get; set; }

        [Navigate(typeof(RoleMenuTB), nameof(RoleMenuTB.MenuId), nameof(RoleMenuTB.RoleId))]//注意顺序
        public List<RoleTB> RoleList { get; set; }//只能是null不能赋默认值
    }
}
