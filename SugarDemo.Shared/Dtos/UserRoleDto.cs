using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SugarDemo.Shared
{
    /// <summary>
    /// 用户角色
    /// </summary>
    public class UserRoleDto : BaseDto
    {
        [SugarColumn(ColumnDescription = "用户Id")]
        public int UserId
        {
            get;
            set;
        }
        [SugarColumn(ColumnDescription = "角色Id")]
        public int RoleId
        {
            get;
            set;
        }

        [SugarColumn(ColumnDescription = "是否删除")]
        public int State { get; set; }
    }

    public class UserBatchModel
    {
        public List<UserRoleDto>? AddModel { get; set; }
        public List<UserRoleDto>? DelModel { get; set; }
        public UserDto? Model { get; set; }
    }
}
