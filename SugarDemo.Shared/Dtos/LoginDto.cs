﻿namespace SugarDemo.Shared
{
    public class LoginDto
    {
        public string? JwtToken { get; set; }

        public int UserId { get; set; }
        public string? UserName { get; set; }
    }

    public class LoginInput
    {
        public string Account { get; set; }
        public string Password { get; set; }
    }
}
