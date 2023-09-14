using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Furion;
using Microsoft.Extensions.DependencyInjection;

namespace WPFTestApi
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Serve.RunNative(services =>
            {
                services.AddRemoteRequest(options =>
                {
                    // 配置 Github 基本信息
                    options.AddHttpClient("github", c =>
                    {
                        c.BaseAddress = new Uri("https://localhost:44342/");
                        //c.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
                        //c.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory-Sample");
                    });
                });
            });
        }
    }
}
