namespace MySqlSugar.Models
{
    [SugarTable(tableName: "RoleMenuTB")]
    public class RoleMenuTB : BaseTable
    {
        [SugarColumn(IsPrimaryKey = true)]
        public int RoleId { get; set; }

        [SugarColumn(IsPrimaryKey = true)]
        public int MenuId { get; set; }
    }
}
