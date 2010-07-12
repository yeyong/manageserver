using System;
using System.Collections.Generic;
using System.Text;

namespace SAS.Plugin.TaoBao
{
    public class TaoBaoPluginProvider
    {
        private static TaoBaoPluginBase _sp;

        private TaoBaoPluginProvider(){}

        static TaoBaoPluginProvider()
        {
            try
            {
                _sp = (TaoBaoPluginBase)Activator.CreateInstance(Type.GetType("SAS.Taobao.TaoBaoPlugin, SAS.Taobao", false, true));
            }
            catch
            {
                _sp = null;
            }
        }

        public static TaoBaoPluginBase GetInstance()
        {
            return _sp;
        }
    }
}
