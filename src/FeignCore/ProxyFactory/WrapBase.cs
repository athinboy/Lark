using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Feign.Core.ProxyFactory
{
    public class WrapBase
    {
        public string Url { get; set; }
        public HttpResponseMessage Response { get; internal set; }
    }
}
