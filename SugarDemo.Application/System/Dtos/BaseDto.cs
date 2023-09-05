namespace SugarDemo.Application
{
    public class BaseDto : INotifyPropertyChanged
    {
        [SugarColumn(ColumnDescription = "是否删除")]
        public int IsDel { get; set; }

        [SugarColumn(ColumnDescription = "创建人")]
        public string CreateBy { get; set; }

        [SugarColumn(ColumnDescription = "创建日期")]
        public DateTime CreateDate { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 实现通知更新
        /// </summary>
        /// <param name="propertyName"></param>
        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
