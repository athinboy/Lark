using Feign.Core.Exception;
using Feign.Core.ProxyFactory;
using FeignCore.ValueBind;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Feign.Core.Context
{
    internal class RequestCreContext : ContextBase
    {


        private RequestCreContext(InterfaceWrapContext interfaceWrap, MethodWrapContext methodWrap, WrapBase wrapinstance)
        {
            this.InfaceContext = interfaceWrap;
            this.MethodWrap = methodWrap;
            this.WrapInstance = wrapinstance;
        }

        internal static RequestCreContext Create(InterfaceWrapContext interfaceWrap, MethodWrapContext methodWrap, WrapBase wrapinstance)
        {

            RequestCreContext instance = new RequestCreContext(interfaceWrap, methodWrap, wrapinstance);
            instance.CreateHeaderBind();
            return instance;
        }

        public InterfaceWrapContext InfaceContext { get; set; }
        public MethodWrapContext MethodWrap { get; set; }

        public WrapBase WrapInstance { get; set; }

        public HttpRequestMessage httpRequestMessage;

        public List<HeaderBind> HeaderBindes { get; set; } = new List<HeaderBind>();

        //todo performance ok ?
        public string GetRequestUrl()
        {
            //todo is it need to check url begin/end with “/”？？
            string url = this.WrapInstance.Url + this.MethodUrl + (this.QueryString.Count > 0 ? "?" : string.Empty);

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
            if (ParameterValues.Value != null)
            {
                ParameterValues.Value.Clear();
            }
            methodUrl = string.Empty;
            throw new NotImplementedException();

        }

        internal void FillPath(string name, string valueStr)
        {
            this.methodUrl = this.MethodUrl.Replace("{" + name + "}", valueStr);

        }

        public HttpMethod HttpMethod
        {
            get
            {
                return new HttpMethod(this.MethodWrap.HttpMethod);
            }
        }
        public System.Threading.ThreadLocal<List<object>> ParameterValues { get; internal set; } = new System.Threading.ThreadLocal<List<object>>();
        public Dictionary<string, string> QueryString { get; internal set; } = new Dictionary<string, string>();

        private string methodUrl = string.Empty;



        private void CreateHeaderBind()
        {
            this.HeaderBindes.Clear();
            this.InfaceContext.HeaderBindes.ForEach(x =>
            {
                this.HeaderBindes.Add((HeaderBind)x.Clone());
            });
            this.MethodWrap.HeaderBindes.ForEach(x =>
            {
                this.HeaderBindes.ForEach(hb =>
                {
                    if (hb.Name == x.Name && (x.Unique || hb.Unique))
                    {
                        hb.Enable = false;
                    }
                });

                this.HeaderBindes.Add((HeaderBind)x.Clone());
            });

            this.MethodWrap.ParameterCache.ForEach(para =>
            {
                para.HeaderBindes.ForEach(x =>
                {
                    this.HeaderBindes.ForEach(hb =>
                    {
                        if (hb.Name == x.Name && (x.Unique || hb.Unique))
                        {
                            hb.Enable = false;
                        }
                    });
                    this.HeaderBindes.Add((HeaderBind)x.Clone());
                });
            });


        }

        public string MethodUrl
        {
            get
            {
                if (methodUrl.Length == 0)
                {
                    methodUrl = this.MethodWrap.MethodPath;
                }
                return methodUrl;

            }


        }


        public HttpRequestMessage PreparaRequestMessage()
        {
            SpeculateRequestMessage();

            this.HeaderBindes.ForEach(x =>
            {
                x.AddHeader(this);
            });

            ParameterWrapContext parameterWrap;

            for (int i = 0; i < this.MethodWrap.ParameterCache.Count; i++)
            {
                parameterWrap = this.MethodWrap.ParameterCache[i];

                parameterWrap.AddQueryString(this);
                parameterWrap.FillPath(this);

                if (InternalConfig.LogRequest)
                {
                    parameterWrap.Serial(this.ParameterValues.Value[parameterWrap.Parameter.Position]);
                }
            }


            return this.httpRequestMessage;

        }



        private HttpRequestMessage SpeculateRequestMessage()
        {

            HttpRequestMessage httpRequestMessage = new HttpRequestMessage();             

            httpRequestMessage.Content = this.MethodWrap.bodyBind.Bindbody(this);

            this.httpRequestMessage = httpRequestMessage;
            return httpRequestMessage;

        }
        internal override void CreateBind()
        {

        }
    }
}
