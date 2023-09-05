namespace MySqlSugar.Models
{
    [SugarTable("LogTB")]
    public class LogTB
    {
        [SugarColumn(IsIdentity = true, IsPrimaryKey = true)]
        public int LogId { get; set; }

        [SugarColumn(ColumnDescription = "名称", Length = 50)]
        public string LogName { get; set; }

        [SugarColumn(ColumnDescription = "描述", Length =200)]
        public string LogDesc { get; set; }

        [SugarColumn(ColumnDescription = "创建日期")]
        public DateTime CreateDate { get; set; }  
    }
}
