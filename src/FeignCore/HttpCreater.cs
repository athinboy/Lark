
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


        internal static FeignResult Create(RequestCreContext requestCreContext)
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


            interfaceWrap.AddHeader(requestCreContext);


            methodWrap.AddHeader(requestCreContext);
            methodWrap.AddQueryString(requestCreContext);


            for (int i = 0; i < methodWrap.ParameterCache.Count; i++)
            {
                parameterWrap = methodWrap.ParameterCache[i];
                parameterWrap.AddHeader(requestCreContext);
                parameterWrap.AddQueryString(requestCreContext);
                parameterWrap.FillPath(requestCreContext);

                if (InternalConfig.LogRequest)
                {
                    parameterWrap.Serial(requestCreContext.ParaValues[parameterWrap.Parameter.Position]);
                }
            }


            //todo is it ok?
            httpRequestMessage.RequestUri = new Uri(requestCreContext.GetRequestUrl());

            System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient();

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
            if (InternalConfig.SaveRequest)
            {
                wrapBase.MyClient = httpClient;
                wrapBase.MyHttpRequestMessagea = httpRequestMessage;
                wrapBase.MyRequestCreContext = requestCreContext;

            }
            if (InternalConfig.NotRequest)
            {
                wrapBase.MyClient = httpClient;
                wrapBase.MyHttpRequestMessagea = httpRequestMessage;
                wrapBase.MyRequestCreContext = requestCreContext;
                return FeignResult.GetResult(null, requestCreContext.MethodWrap.ReturnContext); ;
            }

            task = httpClient.SendAsync(httpRequestMessage);
            //todo need to try-catch ?
            task.Wait();

            if (InternalConfig.SaveResponse)
            {
                requestCreContext.WrapInstance.OriginalResponseMessage = task.Result;
            }

            return FeignResult.GetResult(task.Result, requestCreContext.MethodWrap.ReturnContext);

        }


        //just support string body.
        private static HttpRequestMessage SpeculateRequestMessage(RequestCreContext requestCreContext)
        {

            HttpRequestMessage httpRequestMessage = new HttpRequestMessage();
            List<ParameterWrapContext> parameterContexts = requestCreContext.MethodWrap.ParameterCache;

            string stringbody = "";           
            if (requestCreContext.MethodWrap.IsGet())
            {
                httpRequestMessage.Content = new StringContent(stringbody);
            }
            else if (requestCreContext.MethodWrap.IsPOST())
            {
                httpRequestMessage.Content = new StringContent(stringbody);
            }
            else
            {
                throw new UnsupportException();
            }

            requestCreContext.httpRequestMessage = httpRequestMessage;
            return httpRequestMessage;

        }
    }
}

