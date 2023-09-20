using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using SqlSugar;

namespace SugarDemo.Shared
{
    /// <summary>
    /// 角色
    /// </summary>
    public class RoleMenuDto
    {
        [SugarColumn(ColumnDescription = "角色Id")]
        public int RoleId
        {
            get; set;
        }

        [SugarColumn(ColumnDescription = "菜单Id")]
        public int MenuId
        {
            get; set;
        }

        [SugarColumn(ColumnDescription = "是否删除,1标识删除，0未删除")]
        public int State
        {
            get; set;
        }

        [SugarColumn(ColumnDescription = "创建人")]
        public string CreateBy
        {
            get; set;
        }

        [SugarColumn(ColumnDescription = "创建时间")]
        public DateTime CreateDate
        {
            get; set;
        }
    }

    public class RoleBatchModel
    {
        public List<RoleMenuDto>? AddModel { get; set; }
        public List<RoleMenuDto>? DelModel { get; set; }
        public RoleDto? Model { get; set; }
    }
}
