using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Text;
using Feign.Core.Attributes;
using Feign.Core.Cache;
using Feign.Core.Exception;
using FeignCore;

namespace Feign.Core.Context
{
    internal class MethodWrapContext : ContextBase
    {
        public Type interfaceType { get; set; }

        public MethodInfo methodInfo { get; set; }

        private string httpMethod = DefaultConfig.DefaultHttpMethod;

        private string url = null;

        /// <summary>
        /// 接口URL 特性
        /// </summary>
        public URLAttribute URLAttribute { get; set; }

        public List<HeaderAttribute> HeaderAttributes { get; set; } = new List<HeaderAttribute>();

        public List<ParameterWrapContext> ParameterCache { get; set; } = new List<ParameterWrapContext>();

        /// <summary>
        /// 方法的URL
        /// </summary>
        public URLAttribute MethodURLAttribute { get; set; }

        private InterfaceWrapContext interfaceWrapContext = null;

        internal override void Clear()
        {
            throw new NotImplementedException();
        }

        private static void SaveParameter(MethodWrapContext methodWrapContext)
        {
            ParameterInfo parameterInfo;
            MethodInfo methodInfo = methodWrapContext.methodInfo;
            ParameterInfo[] parameterInfos = methodInfo.GetParameters();
            Attribute attribute;
            ParameterWrapContext parameterContext;
            BaseAttribute feignAttribute;


            bool asHeader = false;
            bool asQueryStr = false;


            for (int i = 0; i < parameterInfos.Length; i++)
            {

                asHeader = false;
                asQueryStr = false;

                parameterInfo = parameterInfos[i];
                parameterContext = new ParameterWrapContext(methodWrapContext, parameterInfo);
                IEnumerable<Attribute> attributes = parameterInfo.GetCustomAttributes();
                IEnumerator<Attribute> enumerator = attributes.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    attribute = enumerator.Current;
                    if (false == typeof(BaseAttribute).IsInstanceOfType(attribute))
                    {
                        continue;
                    }
                    feignAttribute = attribute as BaseAttribute;

                    if (typeof(HeaderAttribute).IsInstanceOfType(attribute))
                    {
                        asHeader = true;
                        parameterContext.MyHeaderAttributes.Add(feignAttribute as HeaderAttribute);

                    }
                    if (typeof(QueryStringAttribute).IsInstanceOfType(attribute))
                    {
                        asQueryStr = true;
                        parameterContext.MyQueryStringAttributes.Add(feignAttribute as QueryStringAttribute);
                    }

                    feignAttribute.SaveToParameterContext(parameterContext);

                }
                methodWrapContext.ParameterCache.Add(parameterContext);
                if (!asHeader && !asQueryStr)
                {
                    if (methodWrapContext.IsGet())
                    {
                        QueryStringAttribute queryStringAttribute = new QueryStringAttribute();
                        parameterContext.MyQueryStringAttributes.Add(queryStringAttribute);
                        queryStringAttribute.SaveToParameterContext(parameterContext);
                    }
                    if (methodWrapContext.IsPOST())
                    {
                        //todo need to complete

                    }
                }



            }

        }


        internal static MethodWrapContext GetContext(InterfaceWrapContext interfaceWrapContext, MethodInfo methodInfo)
        {
            MethodWrapContext methodWrapContext = new MethodWrapContext();
            methodWrapContext.interfaceType = interfaceWrapContext.InterfaceType;
            methodWrapContext.methodInfo = methodInfo;
            methodWrapContext.interfaceWrapContext = interfaceWrapContext;


            object[] cas = methodInfo.GetCustomAttributes(true);
            BaseAttribute feignAttribute;



            object ca;
            List<string> headers = new List<string>();
            HeaderAttribute headerAttribute;
            for (int i = 0; i < cas.Length; i++)
            {
                ca = cas[i];


                if (false == typeof(BaseAttribute).IsInstanceOfType(ca))
                {

                    continue;
                }

                feignAttribute = ca as BaseAttribute;


                feignAttribute.SaveToMethodContext(methodWrapContext);

                if (typeof(URLAttribute).IsInstanceOfType(ca))
                {
                    methodWrapContext.MethodURLAttribute = ca as URLAttribute;
                    continue;
                }

                if (typeof(HeaderAttribute).IsInstanceOfType(ca))
                {
                    headerAttribute = ca as HeaderAttribute;
                    if (headers.Contains(headerAttribute.Name))
                    {
                        throw new FeignException("重复的Header Name:" + headerAttribute.Name);
                    }
                    else
                    {
                        headers.Add(headerAttribute.Name);
                        methodWrapContext.HeaderAttributes.Add(headerAttribute);
                        continue;
                    }

                }
                if (typeof(MethodAttribute).IsInstanceOfType(ca))
                {
                    methodWrapContext.httpMethod = (ca as MethodAttribute).Method;
                }

            }

            SaveParameter(methodWrapContext);

            methodWrapContext.Validate();

            return methodWrapContext;

        }


        internal override void AddHeader(RequestCreContext requestCreContext, HttpContent httpContext)
        {
            this.HeaderAttributes.ForEach(x =>
            {
                x.AddMethodHeader(requestCreContext, this, httpContext);
            });

        }





        private void Validate()
        {

            if (this.MethodURLAttribute == null
                && string.IsNullOrEmpty(this.httpMethod)
                && this.interfaceWrapContext.URLAttribute != null)
            {
                throw new FeignException("URL and Method Attribute can not be both Null ！");
            }
            if (false == DefaultConfig.SupportHttpMethod.Contains(this.httpMethod))
            {
                throw new FeignException("Just support GET/POST now！");
            }

        }


        public string Url
        {
            get
            {
                if (url == null)
                {
                    url = this.interfaceWrapContext.URLAttribute == null ? null : this.interfaceWrapContext.URLAttribute.Url;
                    url += this.URLAttribute == null ? null : this.URLAttribute.Url;
                    string queryString = url.Contains("?") ? "&" : "?";//TODO:need to check and validate 
                    bool addQuery = false;
                    ParameterCache.ForEach(x =>
                    {
                        queryString += x.QueryString;
                        queryString += "&";
                        addQuery = true;
                    });
                    if (addQuery)
                    {
                        queryString.Remove(queryString.Length - 1);
                    }
                    url +=

                    url = Util.NormalizeURL(url);

                }
                return url;
            }
            private set { }


        }

        public bool JsonBody { get; internal set; } = true;
        public bool XmlBody { get; internal set; } = false;

        public string HttpMethod()
        {
            return httpMethod;
        }
        public bool IsGet()
        {
            return this.httpMethod == "GET";
        }
        public bool IsPOST()
        {
            return this.httpMethod == "POST";
        }






    }
}