using Feign.Core.Context;
using System.Collections.Generic;
using System.Reflection;

namespace Feign.Core.Cache {
    internal class MethodItem {
        internal MethodItem (MethodInfo method, MethodWrapContext wrapContext) {
            this.Method = method;
            this.WrapContext = wrapContext;

        }
        public MethodInfo Method { get; set; }
        public MethodWrapContext WrapContext { get; set; }





    }
}