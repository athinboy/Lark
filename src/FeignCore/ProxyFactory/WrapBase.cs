using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Feign.Core.Context;

namespace Feign.Core.ProxyFactory
{
    //todo Warn:The instance of this class will ba cached . so set to threadlocal ? create once for every request ?  
    public class WrapBase
    {
        public string Url { get; set; }

        /// <summary>
        ///  the http request , just test/debug propose。 
        /// </summary>
        public HttpClient MyClient { get; set; }


        /// <summary>
        ///  the original response from server
        /// </summary>
        public HttpResponseMessage OriginalResponseMessage { get; internal set; }
        /// <summary>
        ///  the http request , just test/debug propose。 
        /// </summary>


        /// <summary>
        ///  the http request , just test/debug propose。 
        /// </summary>
        public HttpRequestMessage MyHttpRequestMessagea { get; internal set; }

        

        /// <summary>
        ///  RequestCreContext , just test/debug propose。 
        /// </summary>
        internal RequestCreContext MyRequestCreContext { get; set; }
    }
}
