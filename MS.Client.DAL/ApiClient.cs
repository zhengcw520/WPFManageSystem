using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Client.DAL
{
    public class ApiClient
    {
        public HttpClient Client { get; private set; }

        public ApiClient(HttpClient httpClient)
        {
            httpClient.BaseAddress = new Uri("https://api.github.com/");
            httpClient.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
            httpClient.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory-Sample");
            Client = httpClient;
        }
    }
}
