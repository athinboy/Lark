using Lark.Core.Context;
using System.Collections.Generic;
using System.Reflection;

namespace Lark.Core.Cache
{
    internal class MethodItem
    {
        internal MethodItem(MethodInfo method, MethodWrapContext wrapContext)
        {
            Method = method;
            WrapContext = wrapContext;

        }
        public MethodInfo Method { get; set; }
        public MethodWrapContext WrapContext { get; set; }

    }
}