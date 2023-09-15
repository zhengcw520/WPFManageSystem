
using Microsoft.Extensions.DependencyInjection;
using SqlSugar;
using System;
using System.Linq;
using System.Reflection;
using DbType = SqlSugar.DbType;

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

        var sqlSugar = new SqlSugarClient(configs);
        if (1 == 0)
        {
            InitTable(sqlSugar);
        }
        services.AddSqlSugar(configs,
        db =>
        {
            db.QueryFilter.AddTableFilter<IDeletedFilter>(it => it.IsDel == 0);
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

    private static void InitTable(ISqlSugarClient db)
    {
        db.DbMaintenance.CreateDatabase();
        /***批量创建表***/
        //语法1：
        string rootDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        Type[] types = Assembly
                .LoadFrom(rootDirectory+"\\SugarDemo.Core.dll")//如果 .dll报错，可以换成 xxx.exe 有些生成的是exe 
                .GetTypes().Where(it => it.FullName.Contains("TB"))//命名空间过滤，当然你也可以写其他条件过滤
                .ToArray();//断点调试一下是不是需要的Type，不是需要的在进行过滤

        db.CodeFirst.InitTables(types);//根据types创建表
    }

}
