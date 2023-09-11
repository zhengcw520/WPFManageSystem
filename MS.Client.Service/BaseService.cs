using RestSharp;
using MS.Client.IService;
using MS.Client.RestSharp;
using MySqlSugar.Shared;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MS.Client.Service
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class
    {
        private readonly HttpRestClient client;
        private readonly string serviceName;
        public BaseService(HttpRestClient client, string serviceName)
        {
            this.client = client;
            this.serviceName = serviceName;
        }

        public async Task<ApiResponse<TEntity>> AddOrUpdateAsync(TEntity entity)
        {
            BaseRequest request = new BaseRequest();
            request.Method = Method.Post;
            request.Route = $"api/{serviceName}/AddOrUpdate";
            request.Parameter = entity;
            return await client.ExecutePostAsync<TEntity>(request);
        }

        public async Task<ApiResponse> DeleteAsync(int id)
        {
            BaseRequest request = new BaseRequest();
            request.Method = Method.Delete;
            request.Route = $"api/{serviceName}/Delete?id={id}";
            return await client.ExecuteAsync(request);
        }

        public async Task<ApiResponse<List<TEntity>>> GetPageListAsync(FindParameter parameter)
        {
            BaseRequest request = new BaseRequest();
            request.Method = Method.Get;
            request.Route = $"api/{serviceName}/GetPageList?pageIndex={parameter.PageIndex}" +
                $"&pageSize={parameter.PageSize}" +
                $"&search={parameter.Search}";
            return await client.ExecuteGetAsync<List<TEntity>>(request);
        }

        public async Task<ApiResponse<List<TEntity>>> GetAllAsync()
        {
            BaseRequest request = new BaseRequest();
            request.Method = Method.Get;
            request.Route = $"api/{serviceName}/GetAllData";
            return await client.ExecuteGetAsync<List<TEntity>>(request);
        }

        public async Task<ApiResponse<TEntity>> GetFirstOfDefaultAsync(int id)
        {
            BaseRequest request = new BaseRequest();
            request.Method = Method.Get;
            request.Route = $"api/{serviceName}/Get?id={id}";
            return await client.ExecuteGetAsync<TEntity>(request);
        }

        public async Task<ApiResponse> GetPageCountAsync()
        {
            BaseRequest request = new BaseRequest();
            request.Method = Method.Get;
            request.Route = $"api/{serviceName}/GetPageCount";
            return await client.ExecuteAsync(request);
        }
    }
}