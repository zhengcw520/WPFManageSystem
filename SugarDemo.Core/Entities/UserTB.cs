namespace SugarDemo.Core
{
    [SugarTable(tableName : "UserTB")]
    public class UserTB : BaseTable
    {
        [SugarColumn(IsIdentity = true, IsPrimaryKey = true)]
        public int UserId { get; set; }

        [SugarColumn(ColumnDescription = "登录账号", Length = 20)]
        public string Account { get; set; }

        [SugarColumn(ColumnDescription = "用户名", Length = 20)]
        public string UserName { get; set; }

        [SugarColumn(ColumnDescription = "密码")]
        public string Password { get; set; }

        [SugarColumn(ColumnDescription = "图标", Length = 10, IsNullable = true)]
        public string UserIcon { get; set; }

        [SugarColumn(ColumnDescription = "年龄")]
        public int Age { get; set; }

        [SugarColumn(ColumnDescription = "是否启用", DefaultValue = "1")]
        public int IsEnable { get; set; }

        [SugarColumn(ColumnDescription = "地址", Length = 200, IsNullable = true)]
        public string Address { get; set; }

        [SugarColumn(ColumnDescription = "联系电话", Length = 20, IsNullable = true)]
        public string TelPhone { get; set; }

        [Navigate(typeof(UserRoleTB), nameof(UserRoleTB.UserId), nameof(UserRoleTB.RoleId))]//注意顺序
        public List<RoleTB> RoleList { get; set; }//只能是null不能赋默认值
    }
}
