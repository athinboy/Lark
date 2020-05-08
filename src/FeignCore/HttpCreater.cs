
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

 
        internal static string Create(RequestCreContext requestCreContext)
        {

            WrapBase wrapBase = requestCreContext.WrapInstance;

            MethodWrapContext methodWrap = requestCreContext.MethodWrap; 

            if (methodWrap == null)
            {
                throw new ArgumentNullException(nameof(methodWrap));
            }

            InterfaceWrapContext interfaceWrap = requestCreContext.InfaceContext;
            ParameterWrapContext parameterWrap;

            HttpRequestMessage httpRequestMessage = SpeculateRequestMessage(requestCreContext);


            interfaceWrap.AddHeader(requestCreContext, httpRequestMessage.Content);


            methodWrap.AddHeader(requestCreContext, httpRequestMessage.Content);

            methodWrap.AddQueryString(requestCreContext, httpRequestMessage.Content);


            for (int i = 0; i < methodWrap.ParameterCache.Count; i++)
            {
                parameterWrap = methodWrap.ParameterCache[i];
                parameterWrap.AddHeader(requestCreContext, httpRequestMessage.Content);
                parameterWrap.AddQueryString(requestCreContext, httpRequestMessage.Content);

                if (InternalConfig.LogRequest)
                {
                    parameterWrap.Serial(requestCreContext.ParaValues[parameterWrap.Parameter.Position]);
                }
            }


            httpRequestMessage.RequestUri = new Uri(wrapBase.Url + methodWrap.Url);


            System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient();
            HttpResponseMessage httpResponseMessage = null;
            Task<String> taskStr;
            //TODO  it's need to  deal with the http status code
            Task<HttpResponseMessage> task;
            switch (requestCreContext.HttpMethod.Method)
            {
                case "GET":
                case "POST":
                    httpRequestMessage.Method = new HttpMethod(requestCreContext.HttpMethod.Method);
                    break;
                default:
                    throw new NotSupportedException("Not supported Http Method!");
            }
            if (InternalConfig.NotRequest)
            {
                wrapBase.MyClient = httpClient;
                wrapBase.MyHttpRequestMessagea = httpRequestMessage;
                wrapBase.MyRequestCreContext = requestCreContext;
                return null;
            }

            task = httpClient.SendAsync(httpRequestMessage);
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


        //just support string body.
        private static HttpRequestMessage SpeculateRequestMessage(RequestCreContext requestCreContext)
        {

            HttpRequestMessage httpRequestMessage = new HttpRequestMessage();
            List<ParameterWrapContext> parameterContexts = requestCreContext.MethodWrap.ParameterCache;

            string stringbody = "";
            parameterContexts.ForEach(x =>
            {
                if (x.IsBody)
                {

                }
            });
            httpRequestMessage.Content = new StringContent(stringbody);
            return httpRequestMessage;

        }
    }
}

