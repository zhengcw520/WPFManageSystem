using System.Collections.Generic;

namespace MySqlSugar.Shared
{
    public class GlobalEntity
    {
        public static int UserId { get; set; }
        public static string UserName { get; set; }
        public static string? JwtToken { get; set; }
        public static List<MenuDto>? MenuDtos { get; set; }
    }
}
