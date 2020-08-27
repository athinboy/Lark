using Lark.Core.Context;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Lark.Core.Cache
{
    internal class InterfaceItem
    {


        public InterfaceWrapContext WrapContext { get; set; }

        public object WrapInstance { get; set; }

        public Type interfaceType { get; set; }

        internal InterfaceItem(object wrapInstance, Type interfaceType, InterfaceWrapContext context)
        {
            WrapInstance = wrapInstance;
            WrapContext = context;
            this.interfaceType = interfaceType;


        }

    }
}