using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SugarDemo.Shared
{
    public class BaseDto : INotifyPropertyChanged
    {
        [SugarColumn(ColumnDescription = "是否删除")]
        public int IsDel { get; set; }

        [SugarColumn(ColumnDescription = "创建人")]
        public string? CreateBy { get; set; }

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
