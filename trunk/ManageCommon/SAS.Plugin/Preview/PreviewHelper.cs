﻿using System;
using System.Text;
using System.Collections;
using System.IO;

using SAS.Config;
using SAS.Common;
using SAS.Entity;

namespace SAS.Plugin.Preview
{
    /// <summary>
    /// 预览信息辅助类
    /// </summary>
    public class PreviewHelper
    {
        /// <summary>
        /// 获取cache文件夹物理路径，以"\"结束
        /// </summary>
        /// <returns></returns>
        public static string GetPreviewCachePhysicalPath()
        {
            string path = BaseConfigs.GetSitePath + "cache/plugin/preview/";
            return Utils.GetMapPath(path);
        }

        /// <summary>
        /// 创建指定文件夹
        /// </summary>
        /// <param name="dir">文件夹路径</param>
        public static void CreateDirectory(string path)
        {
            Utils.CreateDir(path);
        }

        /// <summary>
        /// 检查文件是否存在
        /// </summary>
        /// <param name="fileName">文件路径</param>
        /// <returns></returns>
        public static bool IsFileExist(string fileName)
        {
            return Utils.FileExists(fileName);
        }

        /// <summary>
        /// 检查文件夹是否存在
        /// </summary>
        /// <param name="path">文件夹物理路径</param>
        /// <returns></returns>
        public static bool IsDirectoryExist(string path)
        {
            return Directory.Exists(path);
        }
    }
}
