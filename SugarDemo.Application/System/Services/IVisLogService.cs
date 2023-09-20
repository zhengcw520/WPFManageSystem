using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SugarDemo.Application
{
    public interface IVisLogService
    {
        public Task<SugarPagedList<VisLogDto>> GetDetailAsync(FindParameter find);
    }
}
