// 麻省理工学院许可证
//
// 版权所有 (c) 2021-2023 zuohuaijun，大名科技（天津）有限公司  联系电话/微信：18020030720  QQ：515096995
//
// 特此免费授予获得本软件的任何人以处理本软件的权利，但须遵守以下条件：在所有副本或重要部分的软件中必须包括上述版权声明和本许可声明。
//
// 软件按“原样”提供，不提供任何形式的明示或暗示的保证，包括但不限于对适销性、适用性和非侵权的保证。
// 在任何情况下，作者或版权持有人均不对任何索赔、损害或其他责任负责，无论是因合同、侵权或其他方式引起的，与软件或其使用或其他交易有关。


namespace SugarDemo.Core;

/// <summary>
/// 数据库日志写入器
/// </summary>
public class DatabaseLoggingWriter : IDatabaseLoggingWriter
{
    private readonly IRepository<SysLogVisTB> _sysLogVisRep; // 访问日志
    private readonly IRepository<SysLogOpTB> _sysLogOpRep;   // 操作日志
    private readonly IRepository<SysLogExTB> _sysLogExRep;   // 异常日志
    //private readonly SysConfigService _sysConfigService; // 参数配置服务

    public DatabaseLoggingWriter(IRepository<SysLogVisTB> sysLogVisRep,
        IRepository<SysLogOpTB> sysLogOpRep,
        IRepository<SysLogExTB> sysLogExRep//,
        //SysConfigService sysConfigService
        )
    {
        _sysLogVisRep = sysLogVisRep;
        _sysLogOpRep = sysLogOpRep;
        _sysLogExRep = sysLogExRep;
        //_sysConfigService = sysConfigService;
    }

    public async void Write(LogMessage logMsg, bool flush)
    {
        var jsonStr = logMsg.Context.Get("loggingMonitor").ToString();
        var loggingMonitor = JsonConvert.DeserializeObject<dynamic>(jsonStr);

        // 不记录数据校验日志
        if (loggingMonitor.validation != null) return;

        // 获取当前操作者
        string account = "", realName = "", userId = "";
        //string tenantId = "";
        if (loggingMonitor.authorizationClaims != null)
        {
            foreach (var item in loggingMonitor.authorizationClaims)
            {
                if (item.type == ClaimConst.Account)
                    account = item.value;
                if (item.type == ClaimConst.UserId)
                    userId = item.value;
            }
        }

        string remoteIPv4 = loggingMonitor.remoteIPv4;
        (string ipLocation, double? longitude, double? latitude) = GetIpAddress(remoteIPv4);

        var client = Parser.GetDefault().Parse(loggingMonitor.userAgent.ToString());
        var browser = $"{client.UA.Family} {client.UA.Major}.{client.UA.Minor} / {client.Device.Family}";
        var os = $"{client.OS.Family} {client.OS.Major} {client.OS.Minor}";

        // 记录异常日志并发送邮件
        if (logMsg.Exception != null || loggingMonitor.exception != null)
        {
            await _sysLogExRep.InsertAsync(new SysLogExTB
            {
                ControllerName = loggingMonitor.controllerName,
                ActionName = loggingMonitor.actionTypeName,
                DisplayTitle = loggingMonitor.displayTitle,
                Status = loggingMonitor.returnInformation?.httpStatusCode,
                RemoteIp = remoteIPv4,
                Location = ipLocation,
                Longitude = longitude,
                Latitude = latitude,
                Browser = browser, // loggingMonitor.userAgent,
                Os = os, // loggingMonitor.osDescription + " " + loggingMonitor.osArchitecture,
                Elapsed = loggingMonitor.timeOperationElapsedMilliseconds,
                LogDateTime = logMsg.LogDateTime,
                Account = account,
                RealName = realName,
                HttpMethod = loggingMonitor.httpMethod,
                RequestUrl = loggingMonitor.requestUrl,
                RequestParam = (loggingMonitor.parameters == null || loggingMonitor.parameters.Count == 0) ? string.Empty : JSON.Serialize(loggingMonitor.parameters[0].value),
                ReturnResult = loggingMonitor.returnInformation == null ? null : JSON.Serialize(loggingMonitor.returnInformation),
                EventId = logMsg.EventId.Id,
                ThreadId = logMsg.ThreadId,
                TraceId = logMsg.TraceId,
                Exception = JSON.Serialize(loggingMonitor.exception),
                Message = logMsg.Message,
                CreateBy = string.IsNullOrWhiteSpace(userId) ? string.Empty : userId,
                CreateDate = System.DateTime.Now,   
                //TenantId = string.IsNullOrWhiteSpace(tenantId) ? 0 : long.Parse(tenantId),
                LogLevel = logMsg.LogLevel
            });

            // 将异常日志发送到邮件
            try
            {
                //await App.GetRequiredService<SysMessageService>().SendEmail(JSON.Serialize(loggingMonitor.exception));
            }
            catch { }

            return;
        }

        // 记录访问日志-登录登出
        if (loggingMonitor.actionName == "login" || loggingMonitor.actionName == "logout")
        {
            await _sysLogVisRep.InsertAsync(new SysLogVisTB
            {
                ControllerName = loggingMonitor.controllerName,
                ActionName = loggingMonitor.actionTypeName,
                DisplayTitle = loggingMonitor.displayTitle,
                Status = loggingMonitor.returnInformation?.httpStatusCode,
                RemoteIp = remoteIPv4,
                Location = ipLocation,
                Longitude = longitude,
                Latitude = latitude,
                Browser = browser, // loggingMonitor.userAgent,
                Os = os, // loggingMonitor.osDescription + " " + loggingMonitor.osArchitecture,
                Elapsed = loggingMonitor.timeOperationElapsedMilliseconds,
                LogDateTime = logMsg.LogDateTime,
                Account = account,
                RealName = realName,
                CreateBy = string.IsNullOrWhiteSpace(userId) ? string.Empty : userId,
                CreateDate = System.DateTime.Now,
                //TenantId = string.IsNullOrWhiteSpace(tenantId) ? 0 : long.Parse(tenantId),
                LogLevel = logMsg.LogLevel
            });

            return;
        }

        // 记录操作日志
        //var enabledSysOpLog = await _sysConfigService.GetConfigValue<bool>(CommonConst.SysOpLog);
        //if (!enabledSysOpLog) return;
        await _sysLogOpRep.InsertAsync(new SysLogOpTB
        {
            ControllerName = loggingMonitor.controllerName,
            ActionName = loggingMonitor.actionTypeName,
            DisplayTitle = loggingMonitor.displayTitle,
            Status = loggingMonitor.returnInformation?.httpStatusCode,
            RemoteIp = remoteIPv4,
            Location = ipLocation,
            Longitude = longitude,
            Latitude = latitude,
            Browser = browser, // loggingMonitor.userAgent,
            Os = os, // loggingMonitor.osDescription + " " + loggingMonitor.osArchitecture,
            Elapsed = loggingMonitor.timeOperationElapsedMilliseconds,
            LogDateTime = logMsg.LogDateTime,
            Account = account,
            RealName = realName,
            HttpMethod = loggingMonitor.httpMethod,
            RequestUrl = loggingMonitor.requestUrl,
            RequestParam = (loggingMonitor.parameters == null || loggingMonitor.parameters.Count == 0) ? string.Empty : JSON.Serialize(loggingMonitor.parameters[0].value),
            ReturnResult = loggingMonitor.returnInformation == null ? null : JSON.Serialize(loggingMonitor.returnInformation),
            EventId = logMsg.EventId.Id,
            ThreadId = logMsg.ThreadId,
            TraceId = logMsg.TraceId,
            Exception = loggingMonitor.exception == null ? string.Empty : JSON.Serialize(loggingMonitor.exception),
            Message = logMsg.Message,
            CreateBy = string.IsNullOrWhiteSpace(userId) ? string.Empty : userId,
            //TenantId = string.IsNullOrWhiteSpace(tenantId) ? 0 : long.Parse(tenantId),
            LogLevel = logMsg.LogLevel
        });
    }

    /// <summary>
    /// 解析IP地址
    /// </summary>
    /// <param name="ip"></param>
    /// <returns></returns>
    private static (string ipLocation, double? longitude, double? latitude) GetIpAddress(string ip)
    {
        try
        {
            var ipInfo = IpTool.Search(ip);
            var addressList = new List<string>() { ipInfo.Country, ipInfo.Province, ipInfo.City, ipInfo.NetworkOperator };
            return (string.Join("|", addressList.Where(it => it != "0").ToList()), ipInfo.Longitude, ipInfo.Latitude); // 去掉0并用|连接
        }
        catch { }
        return ("未知", 0, 0);
    }
}