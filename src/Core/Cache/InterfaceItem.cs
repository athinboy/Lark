using System;
using System.Collections.Generic;
using System.Reflection;

namespace Feign.Core.Cache {
    internal class InterfaceItem {
        //todo 会有多线程访问的问题 System.Collections.Concurrent.ConcurrentDictionary
        public Dictionary<MethodInfo, MethodItem> MethodCache { get; set; }

        public InterfaceWrapContext wrapContext { get; set; }

        public Object WrapInstance { get; set; }

        public Type interfaceType { get; set; }

        internal InterfaceItem (Object wrapInstance, Type interfaceType, Dictionary<MethodInfo, MethodItem> methodCache, InterfaceWrapContext context) {
            this.WrapInstance = wrapInstance;
            this.wrapContext = context;
            this.interfaceType = interfaceType;
            this.MethodCache = methodCache;

        }

    }
}