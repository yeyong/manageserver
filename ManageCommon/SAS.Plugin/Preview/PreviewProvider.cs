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
    /// 预览信息对象创建类
    /// </summary>
    public class PreviewProvider
    {
        private static Hashtable _instance = new Hashtable();
        private static object lockHelper = new object();

        public static IPreview GetInstance(string extname)
        {
            if (!_instance.ContainsKey(extname))
            {
                lock (lockHelper)
                {
                    if (!_instance.ContainsKey(extname))
                    {
                        IPreview p = null;
                        try
                        {
                            p = (IPreview)Activator.CreateInstance(Type.GetType(string.Format("SAS.Plugin.Preview.{0}.Viewer, SAS.Plugin.Preview.{0}", extname), false, true));
                        }
                        catch
                        {
                            p = null;
                        }
                        _instance.Add(extname, p);
                    }
                }
            }
            return (IPreview)_instance[extname];
        }
    }
}
