using System;
using System.Collections.Generic;
using System.Text;

namespace SAS.Plugin
{
    public interface IFeed
    {
        string GetFeed(int ttl, int uid);

        string GetFeed(int ttl);
    }
}
