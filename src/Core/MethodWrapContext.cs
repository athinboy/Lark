using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Feign.Core.Attributes;
using Feign.Core.Exception;

namespace Feign.Core {
    internal class MethodWrapContext {
        public Type interfaceType { get; set; }

        public MethodInfo methodInfo { get; set; }

        public List<HeaderAttribute> HeaderAttributes { get; set; } = new List<HeaderAttribute> ();

        /// <summary>
        /// 接口URL
        /// </summary>
        public URLAttribute InterfaceURLAttribute { get; set; }

        /// <summary>
        /// 方法的URL
        /// </summary>
        public URLAttribute MethodURLAttribute { get; set; }

        internal static MethodWrapContext GetContext (InterfaceWrapContext interfaceWrapContext, MethodInfo methodInfo) {
            MethodWrapContext methodWrapContext = new MethodWrapContext ();
            methodWrapContext.interfaceType = interfaceWrapContext.interfaceType;
            methodWrapContext.methodInfo = methodInfo;

            methodWrapContext.InterfaceURLAttribute = interfaceWrapContext.InterfaceURLAttribute;

            object[] cas = methodInfo.GetCustomAttributes (true);

            object ca;
            List<string> headers = new List<string> ();
            HeaderAttribute headerAttribute;
            for (int i = 0; i < cas.Length; i++) {
                ca = cas[i];
                if (typeof (URLAttribute).IsInstanceOfType (ca)) {
                    methodWrapContext.MethodURLAttribute = ca as URLAttribute;
                    continue;
                }

                if (typeof (HeaderAttribute).IsInstanceOfType (ca)) {
                    headerAttribute = ca as HeaderAttribute;
                    if (headers.Contains (headerAttribute.Name)) {
                        throw new FeignException ("重复的Header Name:" + headerAttribute.Name);
                    } else {
                        headers.Add (headerAttribute.Name);
                        methodWrapContext.HeaderAttributes.Add (headerAttribute);
                        continue;
                    }

                }

            }

            for (int i = 0; i < interfaceWrapContext.HeaderAttributes.Count; i++) {
                headerAttribute = interfaceWrapContext.HeaderAttributes[i];
                if (headers.Contains (headerAttribute.Name)) {

                } else {
                    headers.Add (headerAttribute.Name);
                    methodWrapContext.HeaderAttributes.Add (headerAttribute);
                }
            }

            methodWrapContext.Validate ();

            return methodWrapContext;

        }

        private void Validate () {

            if (this.MethodURLAttribute == null && this.InterfaceURLAttribute == null) {
                throw new FeignException ("Miss URL Attribute ！");
            }

        }

        private string url = null;

        private string GetUrl () {

            if (url == null) {
                if (null == this.MethodURLAttribute) { }
                if (this.InterfaceURLAttribute == null) {

                }
            }
            return url;

        }

    }
}