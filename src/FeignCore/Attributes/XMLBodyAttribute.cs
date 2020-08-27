using System;
using Feign.Core.Attributes;
using Feign.Core.Context;
using Feign.Core.Enum;

namespace FeignCore.Attributes
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