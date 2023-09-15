namespace SugarDemo.Core
{
    public class BaseTable:IDeletedFilter
    {
        [SugarColumn(ColumnDescription = "是否删除", DefaultValue = "0")]
        public virtual int IsDel { get; set; } = 0;

        [SugarColumn(ColumnDescription = "创建人", Length = 20)]
        public string CreateBy { get; set; }

        [SugarColumn(ColumnDescription = "创建日期")]
        public DateTime CreateDate { get; set; }
    }
}
