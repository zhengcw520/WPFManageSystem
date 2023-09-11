using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySqlSugar.Shared
{
    /// <summary>
    /// 菜单
    /// </summary>
    public class UpgradeFileDto : BaseDto
    {
        [DisplayName("文件名称")]
        public string FileName
        {
            get;
            set;
        }

        [DisplayName("文件Md5")]
        public string FileMd5
        {
            get;
            set;
        }

        [DisplayName("文件路径")]
        public string FilePath
        {
            get;
            set;
        }

        [DisplayName("上传时间")]
        public string UploadTime
        {
            get;
            set;
        }
        [DisplayName("状态")]
        public int State
        {
            get; set;
        }
    }
}
