using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SugarDemo.Application
{
    [DisplayName("登录服务")]
    public class LoginAppService : IDynamicApiController
    {
        private readonly ILoginService _service;
        public LoginAppService(ILoginService service)
        {
            _service = service; 
        }

        [HttpPost]
        [AllowAnonymous]
        [DisplayName("登录系统")]
        public async Task<LoginDto> LoginAsync(LoginInput input) 
        {
            return await _service.LoginAsync(input);
        }

        [HttpPost]
        [DisplayName("注册系统")]
        public async Task RegisterAsync(UserDto user) 
        {
            await _service.RegisterAsync(user);  
        }
    }
}
