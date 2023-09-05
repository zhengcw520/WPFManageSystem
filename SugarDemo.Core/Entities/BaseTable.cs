namespace MySqlSugar.Models
{
    public class BaseTable
    {
        [SugarColumn(ColumnDescription = "是否删除", DefaultValue = "0")]
        public int IsDel { get; set; }

        [SugarColumn(ColumnDescription = "创建人", Length = 20)]
        public string CreateBy { get; set; }

        [SugarColumn(ColumnDescription = "创建日期")]
        public DateTime CreateDate { get; set; }
    }
}
