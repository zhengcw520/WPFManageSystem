﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SugarDemo.Application
{
    [ErrorCodeType]
    public enum ErrorCodes
    {
        [ErrorCodeItemMetadata("{0} 不能小于 {1}")]
        z1000,

        [ErrorCodeItemMetadata("数据不存在")]
        x1000,

        [ErrorCodeItemMetadata("注册失败")]
        x1002,

        [ErrorCodeItemMetadata("{0} 发现 {1} 个异常", "百小僧", 2)]
        x1001,

        [ErrorCodeItemMetadata("服务器运行异常", ErrorCode = "Error")]
        SERVER_ERROR
    }
}
