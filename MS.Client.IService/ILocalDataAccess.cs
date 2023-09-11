using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Client.IService
{
    public interface ILocalDataAccess
    {
        List<string[]> GetLocalFileInfo();
        List<string> GetIcons();
    }
}
