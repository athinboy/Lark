using Feign.Core.ProxyFactory;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Feign.Core.Context
{
    internal class RequestCreContext : ContextBase
    {
        public InterfaceWrapContext InfaceContext { get; set; }
        public MethodWrapContext MethodWrap { get; set; }

        public WrapBase WrapInstance { get; set; }

        public HttpRequestMessage httpRequestMessage;


        //todo performance ok ?
        public string GetRequestUrl()
        {
            string url = this.WrapInstance.Url + this.MethodWrap.Url + "?";

            foreach (var item in this.QueryString)
            {
                url += (item.Key + "=" + item.Value + "&");
            }
            if (url.EndsWith("&"))
            {
                url = url.Remove(url.Length - 1);
            }
            return url;
        }


        internal override void Clear()
        {
            QueryString.Clear();
            ParaValues.Clear();
            throw new NotImplementedException();

        }

        public HttpMethod HttpMethod
        {
            get
            {
                return new HttpMethod(this.MethodWrap.HttpMethod());
            }
        }
        public List<object> ParaValues { get; internal set; }
        public Dictionary<string, string> QueryString { get; internal set; } = new Dictionary<string, string>();
    }
}
