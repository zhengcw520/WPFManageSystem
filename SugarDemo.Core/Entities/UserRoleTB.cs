namespace MySqlSugar.Models
{
    [SugarTable(tableName: "UserRoleTB")]
    public class UserRoleTB : BaseTable
    {
        [SugarColumn(IsPrimaryKey = true)]
        public int UserId { get; set; }

        [SugarColumn(IsPrimaryKey = true)]
        public int RoleId { get; set; }
    }
}
