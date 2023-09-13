namespace MS.Client.DAL
{
    public class BaseDAL<TEntity> : IBaseDAL<TEntity> where TEntity : class
    {
        private readonly string DALName;
        private readonly string ApiName;
        public BaseDAL(string DALName,string ApiName)
        {
            this.DALName = DALName;
            this.ApiName = ApiName;
        }

        public async Task<FurApiResponse> AddOrUpdateAsync(TEntity entity)
        {
            //https://localhost:44342/api/menu/or-update
            return await "api/{DALName}/or-update".SetClient(ApiName).SetBody(entity).PostAsAsync<FurApiResponse>();
        }

        public async Task<FurApiResponse> DeleteAsync(int id)
        {
            //https://localhost:44342/api/menu/1000000
            return await "api/{DALName}/{id}".SetClient(ApiName).PostAsAsync<FurApiResponse>();
        }

        public async Task<FurApiResponse> GetPageListAsync(FindParameter parameter)
        {
            //https://localhost:44342/api/role/page-list?PageIndex=1&PageSize=10
            string exp = $"api/{DALName}/page-list/PageIndex={parameter.PageIndex}" +
                $"&PageSize={parameter.PageSize}" +
                $"&Search={parameter.Search}";
            return await exp.SetClient(ApiName).GetAsAsync<FurApiResponse>();
        }

        public async Task<FurApiResponse> GetAllAsync()
        {
            //https://localhost:44342/api/menu/all
            return await "api/{DALName}/all".SetClient(ApiName).GetAsAsync<FurApiResponse>();
        }

        public async Task<FurApiResponse> GetFirstOfDefaultAsync(int id)
        {
            //https://localhost:44342/api/menu/single/222
            return await $"api/{DALName}/single/{id}".SetClient(ApiName).GetAsAsync<FurApiResponse>();
        }
    }
}