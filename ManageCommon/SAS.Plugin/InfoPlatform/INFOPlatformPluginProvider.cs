using System;
using System.Collections.Generic;
using System.Text;

namespace SAS.Plugin.InfoPlatform
{
    public class INFOPlatformPluginProvider
    {
        private static INFOPlatformPluginBase _sp;

        private INFOPlatformPluginProvider(){}

        static INFOPlatformPluginProvider()
        {
            try
            {
                _sp = (INFOPlatformPluginBase)Activator.CreateInstance(Type.GetType("SAS.InfoRelease.INFOPlatformPlugin, SAS.InfoRelease", false, true));
            }
            catch
            {
                _sp = null;
            }
        }

        public static INFOPlatformPluginBase GetInstance()
        {
            return _sp;
        }
    }
}
