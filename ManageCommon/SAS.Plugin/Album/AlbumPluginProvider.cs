using System;
using System.Collections.Generic;
using System.Text;

namespace SAS.Plugin.Album
{
    public class AlbumPluginProvider
    {
        private static AlbumPluginBase _sp;

        private AlbumPluginProvider() { }

        static AlbumPluginProvider()
        {
            try
            {
                _sp = (AlbumPluginBase)Activator.CreateInstance(Type.GetType("SAS.Album.AlbumPlugin, SAS.Album", false, true));
            }
            catch
            {
                _sp = null;
            }
        }

        public static AlbumPluginBase GetInstance()
        {
            return _sp;
        }
    }
}
