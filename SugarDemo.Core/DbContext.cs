
using Microsoft.Extensions.DependencyInjection;
using SqlSugar;
using System;
using System.Linq;

namespace SugarDemo.Core;

/// <summary>
/// 数据库上下文对象
/// </summary>
public static class DbContext
{
    /// <summary>
    /// SqlSugar 数据库实例
    /// </summary>
    public static void AddSqlSugarSetup(this IServiceCollection services)
    {
        //var configs = App.GetConfig<ConnectionConfig>("default");
        ConnectionConfig configs = new ConnectionConfig() 
        {
            ConnectionString = "Server=WIN-ILJFS06PFQ1;Database=Sqlsugar;User=sa;Password=SQL2022!;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=True;",
            DbType = DbType.SqlServer,
            IsAutoCloseConnection = true
        };
        services.AddSqlSugar(configs,
        db =>
        {
            //处理日志事务
            db.Aop.OnLogExecuting = (sql, pars) =>
            {
                Console.WriteLine(sql);
                Console.WriteLine(string.Join(",", pars?.Select(it => it.ParameterName + ":" + it.Value)));
                Console.WriteLine();
                App.PrintToMiniProfiler("SqlSugar", "Info", sql + "\r\n" + db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
            };
        });

        //services.AddSingleton<ISqlSugarClient>(sqlSugar); // 单例注册
        services.AddScoped(typeof(IRepository<>)); // 仓储注册
    }
}
