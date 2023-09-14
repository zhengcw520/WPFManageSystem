namespace MySqlSugar.Shared
{
    public class LoginInfoModel
    {
        public string? JwtToken { get; set; }

        public int UserId { get; set; }
    }

    public class LoginInput
    {
        public string Account { get; set; }
        public string Password { get; set; }
    }
}
