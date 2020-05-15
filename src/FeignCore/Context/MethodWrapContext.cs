using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Text;
using Feign.Core.Attributes;
using Feign.Core.Cache;
using Feign.Core.Exception;

namespace Feign.Core.Context
{
    internal class MethodWrapContext : ContextBase
    {
        public Type interfaceType { get; set; }

        public MethodInfo methodInfo { get; set; }

        private string httpMethod = DefaultConfig.DefaultHttpMethod;

        private string methodPath = null;

        /// <summary>
        /// 接口URL 特性
        /// </summary>
        public PathAttribute PathAttribute { get; set; }

        public List<HeaderAttribute> HeaderAttributes { get; set; } = new List<HeaderAttribute>();

        public List<ParameterWrapContext> ParameterCache { get; set; } = new List<ParameterWrapContext>();

        /// <summary>
        /// 方法的URL
        /// </summary>
        public PathAttribute MethodPathAttribute { get; set; }

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
            bool asUrlPathStr = false;


            for (int i = 0; i < parameterInfos.Length; i++)
            {

                asHeader = false;
                asUrlPathStr = false;

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
                        parameterContext.HeaderAttributes.Add(feignAttribute as HeaderAttribute);
                    }
                    if (typeof(QueryStringAttribute).IsInstanceOfType(attribute))
                    {
                        asUrlPathStr = true;
                        parameterContext.QueryStringAttributes.Add(feignAttribute as QueryStringAttribute);
                    }
                    if (typeof(PathParaAttribute).IsInstanceOfType(attribute))
                    {
                        asUrlPathStr = true;                  
                        parameterContext.PathParaAttribute = feignAttribute as PathParaAttribute;
                        if(parameterContext.PathParaAttribute.Name.Length==0){
                            parameterContext.PathParaAttribute.Name=parameterInfo.Name;
                        }
                    }

                    feignAttribute.SaveToParameterContext(parameterContext);

                }
                methodWrapContext.ParameterCache.Add(parameterContext);
                if (!asHeader && !asUrlPathStr)
                {
                    if (methodWrapContext.IsGet())
                    {
                        if (methodWrapContext.MethodPath.Contains("{" + parameterInfo.Name + "}"))
                        {
                            PathParaAttribute pathParaAttribute = new PathParaAttribute(parameterInfo.Name);
                            parameterContext.PathParaAttribute = pathParaAttribute;

                        }
                        else
                        {
                            QueryStringAttribute queryStringAttribute = new QueryStringAttribute();
                            queryStringAttribute.Name = parameterInfo.Name;
                            parameterContext.QueryStringAttributes.Add(queryStringAttribute);
                            queryStringAttribute.SaveToParameterContext(parameterContext);
                        }

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

                if (typeof(PathAttribute).IsInstanceOfType(ca))
                {
                    methodWrapContext.MethodPathAttribute = ca as PathAttribute;
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


        internal override void AddHeader(RequestCreContext requestCreContext)
        {
            this.HeaderAttributes.ForEach(x =>
            {
                x.AddMethodHeader(requestCreContext, this);
            });

        }


        private void Validate()
        {

            if (this.MethodPathAttribute == null
                && string.IsNullOrEmpty(this.httpMethod)
                && this.interfaceWrapContext.PathAttribute != null)
            {
                throw new FeignException("URL and Method Attribute can not be both Null ！");
            }
            if (false == DefaultConfig.SupportHttpMethod.Contains(this.httpMethod))
            {
                throw new FeignException("Just support GET/POST now！");
            }

        }


        public string MethodPath
        {
            get
            {
                if (methodPath == null)
                {
                    methodPath = this.interfaceWrapContext.PathAttribute == null ? null : this.interfaceWrapContext.PathAttribute.Path;
                    methodPath += this.PathAttribute == null ? "" : this.PathAttribute.Path;
                }
                return methodPath;
            }
            private set { }


        }


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