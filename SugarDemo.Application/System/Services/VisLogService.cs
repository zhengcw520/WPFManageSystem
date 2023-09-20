namespace SugarDemo.Application
{
    public class VisLogService : IVisLogService,IScoped
    {
        private readonly IRepository<SysLogVisTB> _service;
        public VisLogService(IRepository<SysLogVisTB> service)
        {
            _service = service; 
        }
        public async Task<SugarPagedList<VisLogDto>> GetDetailAsync([FromQuery]FindParameter find)
        {
            var result = await _service.AsQueryable()
                 .WhereIF(!string.IsNullOrEmpty(find.Search), it => it.ActionName!.Equals(find.Search))
                 .OrderBy(u => u.CreateDate)
                 .ToPagedListAsync<SysLogVisTB>(find.PageIndex, find.PageSize);
            return result.Adapt<SugarPagedList<VisLogDto>>();
        }
    }
}
