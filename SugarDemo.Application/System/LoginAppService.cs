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

        [HttpPost]
        [AllowAnonymous]
        public async Task<LoginInfoModel> LoginAsync(LoginInput input) 
        {
            return await _service.LoginAsync(input);
        }

        [HttpPost]
        public async Task RegisterAsync(UserTBDto user) 
        {
            await _service.RegisterAsync(user);  
        }
    }
}
