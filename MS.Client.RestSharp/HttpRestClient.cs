using Newtonsoft.Json;
using RestSharp;
using MySqlSugar.Shared;
using System;
using System.Threading.Tasks;

namespace MS.Client.RestSharp
{
    public class HttpRestClient
    {
        private readonly string apiUrl;
        protected readonly RestClient client;

        public HttpRestClient(string apiUrl)
        {
            this.apiUrl = apiUrl;
            client = new RestClient();
        }

        public async Task<ApiResponse> ExecuteAsync(BaseRequest baseRequest)
        {
            var request = new RestRequest(new Uri(apiUrl + baseRequest.Route), baseRequest.Method);
            if (!string.IsNullOrEmpty(GlobalEntity.JwtToken))
                request.AddHeader("Authorization", string.Format("Bearer {0}", GlobalEntity.JwtToken));
            if (baseRequest.Parameter != null)
                request.AddJsonBody(JsonConvert.SerializeObject(baseRequest.Parameter), baseRequest.ContentType);
            var response = await client.ExecuteAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return JsonConvert.DeserializeObject<ApiResponse>(response.Content);
            else
            {
                return new ApiResponse() 
                {
                    Status = false,
                    Result = null,
                    Message = response.ToString()
                };
            }
        }

        public async Task<ApiResponse<T>> ExecuteGetAsync<T>(BaseRequest baseRequest)
        {
            var request = new RestRequest(new Uri(apiUrl + baseRequest.Route),baseRequest.Method);
            request.AddHeader("Content-Type", baseRequest.ContentType);
            if (!string.IsNullOrEmpty(GlobalEntity.JwtToken))
                request.AddHeader("Authorization", string.Format("Bearer {0}", GlobalEntity.JwtToken));
            if (baseRequest.Parameter != null)
                request.AddParameter("param", JsonConvert.SerializeObject(baseRequest.Parameter), ParameterType.RequestBody);
            var response = await client.ExecuteAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return JsonConvert.DeserializeObject<ApiResponse<T>>(response.Content);
            else
            {
                return new ApiResponse<T>()
                {
                    Status = false,
                    Message = response.ErrorMessage
                };
            }
        }

        public async Task<ApiResponse<T>> ExecutePostAsync<T>(BaseRequest baseRequest)
        {
            var request = new RestRequest(new Uri(apiUrl + baseRequest.Route), baseRequest.Method);
            if (!string.IsNullOrEmpty(GlobalEntity.JwtToken))
                request.AddHeader("Authorization", string.Format("Bearer {0}", GlobalEntity.JwtToken));
            if (baseRequest.Parameter != null)
                request.AddJsonBody(JsonConvert.SerializeObject(baseRequest.Parameter), baseRequest.ContentType);
            var response = await client.ExecuteAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return JsonConvert.DeserializeObject<ApiResponse<T>>(response.Content);
            else
            {
                return new ApiResponse<T>()
                {
                    Status = false,
                    Message = response.ErrorMessage
                };
            }
        }
    }
}