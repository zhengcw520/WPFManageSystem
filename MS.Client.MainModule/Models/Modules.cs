namespace MS.Client.MainModule.Models
{
    public class Modules:BindableBase
    {
        private string code;
        private string name;
        private int auth;
        private string typeName;
        private string nameSpace;

        /// <summary>
        /// 模块图标代码
        /// </summary>
        public string Code
        {
            get { return code; }
            set { code = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 模块名称
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 视图名称
        /// </summary>
        public string TypeName
        {
            get { return typeName; }
            set { typeName = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 命名空间名称
        /// </summary>
        public string NameSpace
        {
            get { return nameSpace; }
            set { nameSpace = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 权限值
        /// </summary>
        public int Auth
        {
            get { return auth; }
            set { auth = value; RaisePropertyChanged(); }
        }
    }
}
