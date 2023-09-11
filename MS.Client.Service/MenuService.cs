using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MS.Client.IService;
using MS.Client.RestSharp;
using MySqlSugar.Shared;

namespace MS.Client.Service
{
    public class MenuService : BaseService<MenuDto>, IMenuService
    {
        private readonly HttpRestClient client;
        private readonly string serviceName = "Menu";

        public MenuService(HttpRestClient client) : base(client, "Menu")
        {
            this.client = client;
        }
    }
}
