using System;
using Lark.Core.Context;
using Lark.Core.Enum;
using Lark.Core.Attributes;
using Lark.Core.Context;

namespace LarkCore.Attributes
{
    /// <summary>
    /// xml string body .
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class XMLBodyAttribute : BaseAttribute
    {
        internal override void SaveToInterfaceContext(InterfaceWrapContext interfaceWrapContext)
        {
            interfaceWrapContext.ContentType = HttpContentTypes.xml;
        }
        internal override void SaveToMethodContext(MethodWrapContext methodWrapContext)
        {
            methodWrapContext.ContentType = HttpContentTypes.xml;
        }
    }
}