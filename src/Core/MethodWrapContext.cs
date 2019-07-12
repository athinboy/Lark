using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Feign.Core.Attributes;
using Feign.Core.Exception;

namespace Feign.Core
{
    internal class MethodWrapContext
    {
        public Type interfaceType { get; set; }

        public MethodInfo methodInfo { get; set; }

        private string httpMethod;

        private string url = null;

        public List<HeaderAttribute> HeaderAttributes { get; set; } = new List<HeaderAttribute>();


        /// <summary>
        /// 方法的URL
        /// </summary>
        public URLAttribute MethodURLAttribute { get; set; }

        private InterfaceWrapContext interfaceWrapContext = null;

        internal static MethodWrapContext GetContext(InterfaceWrapContext interfaceWrapContext, MethodInfo methodInfo)
        {
            MethodWrapContext methodWrapContext = new MethodWrapContext();
            methodWrapContext.interfaceType = interfaceWrapContext.interfaceType;
            methodWrapContext.methodInfo = methodInfo;


            object[] cas = methodInfo.GetCustomAttributes(true);

            ParameterInfo[] parameterInfos = methodInfo.GetParameters();

            object ca;
            List<string> headers = new List<string>();
            HeaderAttribute headerAttribute;
            for (int i = 0; i < cas.Length; i++)
            {
                ca = cas[i];
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

            for (int i = 0; i < interfaceWrapContext.HeaderAttributes.Count; i++)
            {
                headerAttribute = interfaceWrapContext.HeaderAttributes[i];
                if (false == headers.Contains(headerAttribute.Name))
                {
                    headers.Add(headerAttribute.Name);
                    methodWrapContext.HeaderAttributes.Add(headerAttribute);
                }
            }

            methodWrapContext.Validate();

            return methodWrapContext;

        }

        private void Validate()
        {

            if (this.MethodURLAttribute == null && string.IsNullOrEmpty(this.httpMethod) && this.interfaceWrapContext.URLAttribute != null)
            {
                throw new FeignException("URL and Method Attribute can not be both Null ！");
            }

        }


        public string Url()
        {

            if (url == null)
            {
                url = Util.NormalizeURL(this.interfaceWrapContext.URLAttribute == null ? null : this.interfaceWrapContext.URLAttribute.Url);
            }
            return url;

        }

        public string HttpMethod()
        {
            return httpMethod;
        }






    }
}