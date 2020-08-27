using System;
using System.Collections.Generic;
using System.Text;
using Lark.Core.ProxyFactory;

namespace Lark.Core
{
    /// <summary>
    /// just suporrt test.
    /// </summary>
    public static class TestUtil
    {

        public static string GetRequestCreURL(this WrapBase warpbase)
        {
            return warpbase.MyRequestCreContext.httpRequestMessage.RequestUri.ToString();
        }        

    }
}