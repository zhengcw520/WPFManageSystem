namespace SugarDemo.Application
{
    /// <summary>
    /// Log服务
    /// </summary>
    [DisplayName("Log服务")]
    public class VisLogAppService : IDynamicApiController
    {
        private readonly VisLogService _service;
        public VisLogAppService(VisLogService service)
        {
            _service = service;
        }
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="find"></param>
        /// <returns></returns>
        [DisplayName("获取访问日志分页数据")]
        public async Task<SqlSugarPagedList<SysLogVisTBDto>> GetPageListAsync([FromQuery]FindParameter find)
        {
            return await _service.GetDetailAsync(find);
        }
    }
}
