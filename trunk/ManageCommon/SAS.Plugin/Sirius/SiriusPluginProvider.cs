using System;
using System.Collections.Generic;
using System.Text;

namespace SAS.Plugin.Sirius
{
    public class SiriusPluginProvider
    {
        private static SiriusPluginBase _sp;

        private SiriusPluginProvider(){}

        static SiriusPluginProvider()
        {
            try
            {
                _sp = (SiriusPluginBase)Activator.CreateInstance(Type.GetType("SAS.Sirius.SiriusPlugin, SAS.Sirius", false, true));
            }
            catch
            {
                _sp = null;
            }
        }

        public static SiriusPluginBase GetInstance()
        {
            return _sp;
        }
    }
}
