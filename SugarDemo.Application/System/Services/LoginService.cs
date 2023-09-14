namespace SugarDemo.Application
{
    public class LoginService : ILoginService,IScoped
    {
        private readonly IRepository<UserTB> _loginService;
        public LoginService(IRepository<UserTB> loginService)
        {
            _loginService = loginService;   
        }

        [IfException(ErrorCodes.x1000, ErrorMessage = "当前登录用户密码不对")]
        public async Task<LoginInfoModel> LoginAsync(LoginInput input)
        {
            var user = await _loginService.GetFirstAsync(x => x.Account!.Equals(input.Account) && x.Password!.Equals(DESHelper.GetMD5Str(input.Password)));
            if (user == null)
            {
                throw Oops.Oh(ErrorCodes.x1000);
            }

            // 生成Token令牌
            var accessToken = JWTEncryption.Encrypt(new Dictionary<string, object>
            {
                { ClaimConst.UserId, user.UserId },
                { ClaimConst.Account, user.Account },
            });

            return new LoginInfoModel
            {
                 UserId = user.UserId,
                 JwtToken = accessToken,    
            };
        }

        [IfException(ErrorCodes.x1000, ErrorMessage = "当前登录用户密码不对")]
        public async Task RegisterAsync(UserTBDto UserTBDto)
        {
            var model = UserTBDto.Adapt<UserTB>();
            var userModel = await _loginService.GetFirstAsync(x => x.Account!.Equals(model.Account));
            if (userModel != null)
                throw Oops.Oh(ErrorCodes.x1000);
            model.UserName = UserTBDto.UserName;
            model.Account = UserTBDto.Account;
            model.Password = DESHelper.GetMD5Str(UserTBDto.Password!);
            model.CreateDate = DateTime.Now;
            model.CreateBy = "ADMIN";
            var flag = await _loginService.InsertAsync(model);
        }
    }
}
