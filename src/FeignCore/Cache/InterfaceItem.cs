using Feign.Core.Context;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Feign.Core.Cache {
    internal class InterfaceItem {


        public InterfaceWrapContext WrapContext { get; set; }

        public Object WrapInstance { get; set; }

        public Type interfaceType { get; set; }

        internal InterfaceItem (Object wrapInstance, Type interfaceType,  InterfaceWrapContext context) {
            this.WrapInstance = wrapInstance;
            this.WrapContext = context;
            this.interfaceType = interfaceType;
      

        }

    }
}