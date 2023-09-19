namespace SugarDemo.Application
{
    /// <summary>
    /// Log服务
    /// </summary>
    [DisplayName("Log服务")]
    public class OpLogAppService : IDynamicApiController
    {
        private readonly IRepository<SysLogOpTB> _service;
        public OpLogAppService(IRepository<SysLogOpTB> service)
        {
            _service = service;
        }
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="find"></param>
        /// <returns></returns>
        [DisplayName("获取操作日志分页数据")]
        public async Task<SqlSugarPagedList<SysLogOpTBDto>> GetPageListAsync([FromQuery]FindParameter find)
        {
            var result = await _service.AsQueryable()
                .WhereIF(!string.IsNullOrEmpty(find.Search), it => it.ActionName!.Equals(find.Search))
                .OrderBy(u => u.CreateDate)
                .ToPagedListAsync<SysLogOpTB>(find.PageIndex, find.PageSize);
            return result.Adapt<SqlSugarPagedList<SysLogOpTBDto>>();
        }
    }
}
