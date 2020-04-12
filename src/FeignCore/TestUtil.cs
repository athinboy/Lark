using System;
using System.Collections.Generic;
using System.Text;
using Feign.Core.ProxyFactory;

namespace Feign.Core
{
    /// <summary>
    /// just suporrt test.
    /// </summary>
    public static class TestUtil
    {

        public static string GetRequestCreURL(this WrapBase warpbase)
        {
            return warpbase.MyRequestCreContext.URL;
        }



    }
}