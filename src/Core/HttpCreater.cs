
using Feign.Core.Attributes;
using Feign.Core.Context;
using Feign.Core.Exception;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Feign.Core
{
    internal class HttpCreater
    {


        private static List<object> empterArgs = new List<object>();

        internal static string Create(RequestCreContext requestCreContext, List<Object> args)
        {
            MethodWrapContext methodWrap = requestCreContext.MethodWrap;
            args = args ?? empterArgs;

            if (methodWrap == null)
            {
                throw new ArgumentNullException(nameof(methodWrap));
            }

            InterfaceWrapContext interfaceWrap = requestCreContext.InfaceContext;
            ParameterWrapContext parameterWrap;

            HttpContent httpContent = SpeculateHttpContent(requestCreContext);
            FeignAttribute feignAttribute;

            for (int i = 0; i < methodWrap.ParameterCache.Count; i++)
            {
                parameterWrap = methodWrap.ParameterCache[i];
                for (int j = 0; j < parameterWrap.MyFeignAttributes.Count; j++)
                {
                    feignAttribute = parameterWrap.MyFeignAttributes[j];
                    feignAttribute.AddHeader(requestCreContext, httpContent);
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
                feignAttribute.AddHeader(requestCreContext, httpContent);
            }


            for (int i = 0; i < interfaceWrap.MyFeignAttributes.Count; i++)
            {
                feignAttribute = interfaceWrap.MyFeignAttributes[i];
                feignAttribute.AddQueryStr(requestCreContext);
                feignAttribute.AddHeader(requestCreContext, httpContent);

            }

 
    
            for (int i = 0; i < args.Count; i++)
            {
                if (InternalConfig.LogRequestParameter)
                {
                    System.Console.WriteLine(args[i].ToString());
                }
            }

            System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient();
            switch (requestCreContext.HttpMethod.Method)
            {
                case "GET":
                    Task<HttpResponseMessage> task = httpClient.GetAsync(requestCreContext.URL);
                    task.Wait();
                    HttpResponseMessage httpResponseMessage = task.Result;
                    Task<String> taskStr = httpResponseMessage.Content.ReadAsStringAsync();
                    taskStr.Wait();
                    return taskStr.Result;

                case "POST":
                    break;
                default:
                    throw new NotSupportedException("Not supported Http Method!");
            }

            return "";




        }


        //todo 此功能需要进行完善,当前有逻辑漏洞。
        private static HttpContent SpeculateHttpContent(RequestCreContext requestCreContext)
        {

            HttpContent httpContent = null;
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



                httpContent = new StringContent(stringbody);
            }




            return httpContent;

        }
    }
}

