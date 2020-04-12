
using Feign.Core.Attributes;
using Feign.Core.Context;
using Feign.Core.Exception;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Feign.Core.ProxyFactory;
using System.Net;

namespace Feign.Core
{
    internal class HttpCreater
    {


        Microsoft.Extensions.Logging.ILogger logger = LoggerFactory.Create((x) => { x.AddConsole(); }).CreateLogger<HttpCreater>();


        private static List<object> empterArgs = new List<object>();

        internal static string Create(RequestCreContext requestCreContext, List<Object> args)
        {

            WrapBase wrapBase = requestCreContext.WrapInstance;

            MethodWrapContext methodWrap = requestCreContext.MethodWrap;
            args = args ?? empterArgs;

            if (methodWrap == null)
            {
                throw new ArgumentNullException(nameof(methodWrap));
            }

            InterfaceWrapContext interfaceWrap = requestCreContext.InfaceContext;
            ParameterWrapContext parameterWrap;

            HttpRequestMessage httpRequestMessage = SpeculateRequestMessage(requestCreContext);
            FeignAttribute feignAttribute;

            for (int i = 0; i < methodWrap.ParameterCache.Count; i++)
            {
                parameterWrap = methodWrap.ParameterCache[i];
                for (int j = 0; j < parameterWrap.MyFeignAttributes.Count; j++)
                {
                    feignAttribute = parameterWrap.MyFeignAttributes[j];
                    feignAttribute.AddHeader(requestCreContext, httpRequestMessage);
                    feignAttribute.AddQueryStr(requestCreContext);

                }
            }


            for (int i = 0; i < interfaceWrap.MyFeignAttributes.Count; i++)
            {
                feignAttribute = interfaceWrap.MyFeignAttributes[i];


            }

            for (int i = 0; i < methodWrap.MyFeignAttributes.Count; i++)
            {
                feignAttribute = methodWrap.MyFeignAttributes[i];

            }

            for (int i = 0; i < methodWrap.MyFeignAttributes.Count; i++)
            {
                feignAttribute = methodWrap.MyFeignAttributes[i];
                feignAttribute.AddQueryStr(requestCreContext);
                feignAttribute.AddHeader(requestCreContext, httpRequestMessage);
            }


            for (int i = 0; i < interfaceWrap.MyFeignAttributes.Count; i++)
            {
                feignAttribute = interfaceWrap.MyFeignAttributes[i];
                feignAttribute.AddQueryStr(requestCreContext);
                feignAttribute.AddHeader(requestCreContext, httpRequestMessage);

            }



            for (int i = 0; i < args.Count; i++)
            {
                if (InternalConfig.LogRequest)
                {

                }
            }

            System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient();
            //HttpWebRequest  

            if (InternalConfig.NotRequest)
            {
                wrapBase.MyClient = httpClient;
                wrapBase.MyHttpRequestMessagea = httpRequestMessage;
                wrapBase.MyRequestCreContext = requestCreContext;
                return null;
            }


            HttpResponseMessage httpResponseMessage = null;
            Task<String> taskStr;
            //todo  it's need to  deal with the http status code
            Task<HttpResponseMessage> task;
            switch (requestCreContext.HttpMethod.Method)
            {
                case "GET":
                    task = httpClient.SendAsync(httpRequestMessage);
                    break;

                case "POST":
                    task = httpClient.SendAsync(httpRequestMessage);
                    break;
                default:
                    throw new NotSupportedException("Not supported Http Method!");
            }


            task.Wait();
            httpResponseMessage = task.Result;
            taskStr = httpResponseMessage.Content.ReadAsStringAsync();
            taskStr.Wait();

            if (InternalConfig.SaveResponse)
            {
                requestCreContext.WrapInstance.OriginalResponseMessage = httpResponseMessage;
            }

            return taskStr.Result;

        }


        //todo there are holes. need to be perfected.
        private static HttpRequestMessage SpeculateRequestMessage(RequestCreContext requestCreContext)
        {

            HttpRequestMessage httpRequestMessage = null;
            List<ParameterWrapContext> parameterContexts = requestCreContext.MethodWrap.ParameterCache;
            bool isstringbody = false;
            string stringbody = "";
            parameterContexts.ForEach(x =>
            {
                if (x.IsBody)
                {
                    isstringbody = true;
                }
            });

            if (isstringbody)
            {
                httpRequestMessage = new HttpRequestMessage();
                httpRequestMessage.Content = new StringContent(stringbody);
            }

            return httpRequestMessage;

        }
    }
}

