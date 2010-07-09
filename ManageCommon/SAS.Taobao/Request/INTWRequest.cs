﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SAS.Taobao.Request
{
    /// <summary>
    /// 请求接口。
    /// </summary>
    public interface INTWRequest
    {
        /// <summary>
        /// 获取API名称。
        /// </summary>
        /// <returns>API名称</returns>
        string GetApiName();

        /// <summary>
        /// 获取所有的Key-Value形式的文本请求参数字典。其中：
        /// Key: 请求参数名
        /// Value: 请求参数文本值
        /// </summary>
        /// <returns>文本请求参数字典</returns>
        IDictionary<string, string> GetParameters();
    }
}
