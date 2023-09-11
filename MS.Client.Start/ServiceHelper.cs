using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace MS.Client.Start
{
    public sealed class ServiceHelper
    {
        private static string settingPath;

        #region 属性

        /// <summary>
        /// 服务器信息
        /// </summary>
        public static Dictionary<string, string> ServicesInfo
        {
            get;
            set;
        }

        #endregion 属性

        #region 方法

        /// <summary>
        /// 初始化
        /// </summary>
        public static void Init(string directoryPath)
        {
            settingPath = Path.Combine(directoryPath, "config\\Services.xml");
            try
            {
                XElement xDoc = XElement.Load(settingPath);
                if (xDoc != null)
                {
                    IEnumerable<XElement> xServices = xDoc.Elements("service");
                    if (xServices.Count() > 0)
                    {
                        //实例
                        ServicesInfo = new Dictionary<string, string>();
                        foreach (var item in xServices)
                        {
                            string service = item.Attribute("display").Value;
                            //ServiceInfo info = new ServiceInfo()
                            //{
                            //    UpdateService = item.Element("updateService") != null ? item.Element("updateService").Value : string.Empty,
                            //    ServiceBaseUri = item.Element("serviceBaseUri") != null ? item.Element("serviceBaseUri").Value : string.Empty,
                            //};
                            string info = item.Element("serviceBaseUri") != null ? item.Element("serviceBaseUri").Value : string.Empty;
                            ServiceHelper.ServicesInfo.Add(service, info);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        #endregion 方法

        #region 构造

        /// <summary>
        /// 构造
        /// </summary>
        static ServiceHelper()
        {
        }

        #endregion 构造

        //public class ServiceInfo
        //{
        //    public string? UpdateService
        //    {
        //        get;
        //        set;
        //    }

        //    public string? ServiceBaseUri
        //    {
        //        get;
        //        set;
        //    }
        //}
    }
}