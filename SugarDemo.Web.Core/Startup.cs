using Furion;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using SugarDemo.Core;

namespace SugarDemo.Web.Core;

public class Startup : AppStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        // 配置选项
        services.AddProjectOptions();

        services.AddConsoleFormatter();
        //SqlSugar
        services.AddSqlSugarSetup();
        services.AddJwt<JwtHandler>(enableGlobalAuthorize:true);

        services.AddCorsAccessor();

        // Json序列化设置
        static void SetNewtonsoftJsonSetting(JsonSerializerSettings setting)
        {
            setting.DateTimeZoneHandling = DateTimeZoneHandling.Local;
            setting.DateFormatString = "yyyy-MM-dd HH:mm:ss"; // 时间格式化
            setting.ReferenceLoopHandling = ReferenceLoopHandling.Ignore; // 忽略循环引用
                                                                          // setting.ContractResolver = new CamelCasePropertyNamesContractResolver(); // 解决动态对象属性名大写
                                                                          // setting.NullValueHandling = NullValueHandling.Ignore; // 忽略空值
                                                                          // setting.Converters.AddLongTypeConverters(); // long转string（防止js精度溢出） 超过16位开启
                                                                          // setting.MetadataPropertyHandling = MetadataPropertyHandling.Ignore; // 解决DateTimeOffset异常
                                                                          // setting.DateParseHandling = DateParseHandling.None; // 解决DateTimeOffset异常
                                                                          // setting.Converters.Add(new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }); // 解决DateTimeOffset异常
        };

        services.AddControllersWithViews()
              .AddAppLocalization()
              .AddNewtonsoftJson(options => SetNewtonsoftJsonSetting(options.SerializerSettings))
              //.AddXmlSerializerFormatters()
              //.AddXmlDataContractSerializerFormatters()
              .AddInjectWithUnifyResult<AdminResultProvider>();

        // 系统日志
        services.AddLoggingSetup();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseCorsAccessor();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseInject(string.Empty);

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
