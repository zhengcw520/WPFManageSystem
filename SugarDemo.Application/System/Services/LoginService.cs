﻿namespace SugarDemo.Application
{
    public class LoginService : ILoginService,IScoped
    {
        private readonly IRepository<UserTB> _loginService;
        public LoginService(IRepository<UserTB> loginService)
        {
            _loginService = loginService;   
        }

        [IfException(ErrorCodes.x1000, ErrorMessage = "当前登录用户密码不对")]
        public async Task<bool> LoginAsync(string Account, string Password)
        {
            var model = await _loginService.GetFirstAsync(x => x.Account!.Equals(Account) && x.Password!.Equals(Password));
            if (model == null)
            {
                throw Oops.Oh(ErrorCodes.x1000);
            }
            return true;
        }

        [IfException(ErrorCodes.x1000, ErrorMessage = "当前登录用户密码不对")]
        public async Task<bool> RegisterAsync(UserTBDto UserTBDto)
        {
            //try
            //{
            //        var model = UserTBDto.Adapt<UserTB>();
            //        var userModel = await _loginService.GetFirstAsync(x => x.Account!.Equals(model.Account));
            //        if (userModel != null)
            //            return new ApiResponse($"当前账号:{model.UserName}已存在,请重新注册！");
            //        model.UserName = UserTBDto.UserName;
            //        model.Account = UserTBDto.Account;
            //        model.Password = utils.GetMD5Str(utils.GetMD5Str(UserTBDto.Password!) + "$" + model.Account);
            //        model.CreateDate = DateTime.Now;
            //        model.CreateBy = "ADMIN";
            //        await work.UserTB.InsertAsync(model);
            //        if (work.Commit())
            //            return new ApiResponse(true, model);
            //        else
            //            return new ApiResponse("注册失败，请稍后重试");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    return new ApiResponse("注册账号失败：" + ex.Message);
            //}
            return true;
        }
    }
}