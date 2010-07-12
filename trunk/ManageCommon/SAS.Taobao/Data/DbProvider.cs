﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SAS.Taobao.Data
{
    public class DbProvider
    {
        private static readonly DataProvider _dp = new DataProvider();
        private DbProvider()
        { }

        public static DataProvider GetInstance()
        {
            return _dp;
        }
    }
}
