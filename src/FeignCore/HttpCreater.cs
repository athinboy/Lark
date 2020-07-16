
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


            HttpRequestMessage httpRequestMessage = requestCreContext.PreparaRequestMessage();
 

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



    }
}

