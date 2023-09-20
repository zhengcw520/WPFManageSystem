namespace MS.Client.Start
{
    public sealed class UserConfigHelper
    {
        private static string settingPath;

        #region 属性

        public static void Init(string dierctoryPath)
        {
            settingPath = Path.Combine(dierctoryPath, "config\\UserConfig.xml");

            try
            {
                XElement xDoc = XElement.Load(settingPath);
                if (xDoc != null)
                {
                    //XElement xCommon = xDoc.Element("common");
                    //if (xCommon != null)
                    //{
                    //    //主题
                    //    UserConfigHelper.SkinName = xCommon.Element("skinName")
                    //        != null ? xCommon.Element("skinName").Value : string.Empty;
                    //    //语言
                    //    UserConfigHelper.Language = xCommon.Element("language")
                    //        != null ? xCommon.Element("language").Value : string.Empty;
                    //}
                    XElement xLogin = xDoc.Element("login");
                    if (xLogin != null)
                    {
                        //上次登录用户
                        UserConfigHelper.LastLogin = xLogin.Element("last")
                            != null ? xLogin.Element("last").Value : string.Empty;

                        LoginHis = new Dictionary<string, LoginUserInfo>();//实例

                        foreach (XElement item in xLogin.Nodes())
                        {
                            if (item.Element("name") == null ||
                                string.IsNullOrEmpty(item.Element("name").Value))
                            {
                                continue;
                            }

                            LoginUserInfo info = new LoginUserInfo();

                            info.LoginName = item.Element("name")
                                != null ? item.Element("name").Value : string.Empty;
                            info.Pwd = item.Element("pwd")
                                != null ? item.Element("pwd").Value : string.Empty;
                            info.Sign = Convert.ToBoolean((item.Element("sign")
                                != null && !string.IsNullOrEmpty(item.Element("sign").Value))
                                ? item.Element("sign").Value : "false");

                            UserConfigHelper.LoginHis.Add(info.LoginName, info);
                        }
                    }
                    //XElement xKBFilePath = xDoc.Element("KBFilePath");
                    //if (xKBFilePath != null)
                    //{
                    //    UserConfigHelper.KBFilePath = xKBFilePath.Element("path") != null ? xKBFilePath.Element("path").Value : string.Empty;
                    //}
                }
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// 上次登录
        /// </summary>
        public static string LastLogin
        {
            get;
            set;
        }

        /// <summary>
        /// 历史登录
        /// </summary>
        public static Dictionary<string, LoginUserInfo> LoginHis
        {
            get;
            set;
        }

        /// <summary>
        /// 主题
        /// </summary>
        public static string SkinName
        {
            get;
            set;
        }

        /// <summary>
        /// 语言
        /// </summary>
        public static string Language
        {
            get;
            set;
        }

        

        /// <summary>
        /// 快捷方式
        /// </summary>
        public static string ShortCuts
        {
            get;
            set;
        }

        public static string UserVersion
        {
            get;
            set;
        }

        #endregion 属性

        #region 方法

        /// <summary>
        /// 保存登录信息至本地
        /// </summary>
        public static void Save()
        {
            if (File.Exists(UserConfigHelper.settingPath))
            {
                File.Delete(UserConfigHelper.settingPath);
            }
            try
            {
                StringBuilder sbXml = new StringBuilder();

                sbXml.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
                sbXml.Append("<setting>");

                sbXml.Append("<common>");
                sbXml.AppendFormat("<skinName>{0}</skinName>", UserConfigHelper.SkinName);
                sbXml.AppendFormat("<language>{0}</language>", UserConfigHelper.Language);
                sbXml.Append("</common>");

                sbXml.Append("<login>");
                sbXml.AppendFormat("<last>{0}</last>", UserConfigHelper.LastLogin);
                foreach (var item in UserConfigHelper.LoginHis)
                {
                    sbXml.Append("<item>");
                    sbXml.AppendFormat("<name>{0}</name>", item.Value.LoginName);
                    sbXml.AppendFormat("<pwd>{0}</pwd>", item.Value.Pwd);
                    sbXml.AppendFormat("<sign>{0}</sign>", item.Value.Sign);
                    sbXml.Append("</item>");
                }
                sbXml.Append("</login>");
                sbXml.Append("</setting>");

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(sbXml.ToString());
                doc.Save(settingPath);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion 方法

        #region 构造

        /// <summary>
        /// 构造
        /// </summary>
        static UserConfigHelper()
        {
        }

        #endregion 构造

        public class LoginUserInfo
        {
            //登录名
            public string LoginName
            {
                get;
                set;
            }

            //密码
            public string Pwd
            {
                get;
                set;
            }

            //是否自动登录
            public bool Sign
            {
                get;
                set;
            }
        }
    }
}