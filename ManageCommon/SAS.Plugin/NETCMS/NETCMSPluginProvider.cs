using System;
using System.Collections.Generic;
using System.Text;

namespace SAS.Plugin.NETCMS
{
    public class NETCMSPluginProvider
    {
        private static NETCMSPluginBase _sp;

        private NETCMSPluginProvider(){}

        static NETCMSPluginProvider()
        {
            try
            {
                _sp = (NETCMSPluginBase)Activator.CreateInstance(Type.GetType("SAS.NETCMS.NETCMSPlugin, SAS.NETCMS", false, true));
            }
            catch
            {
                _sp = null;
            }
        }

        public static NETCMSPluginBase GetInstance()
        {
            return _sp;
        }
    }
}
