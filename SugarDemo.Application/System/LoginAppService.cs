using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SugarDemo.Application
{
    public class LoginAppService : IDynamicApiController
    {
        private readonly ILoginService _service;
        public LoginAppService(ILoginService service)
        {
            _service = service; 
        }

        public async Task<LoginInfoModel> LoginAsync(string Account, string Password) 
        {
            return await _service.LoginAsync(Account, Password);
        }
        public async Task RegisterAsync(UserTBDto UserTBDto) 
        {
            await _service.RegisterAsync(UserTBDto);  
        }
    }
}
