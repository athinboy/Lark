using System;
using Feign.Core.Attributes;
using Feign.Core.Context;
using Feign.Core.Enum;

namespace FeignCore.Attributes
{
    /// <summary>
    /// json string body.
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class JSONBodyAttribute : BaseAttribute
    {
        internal override void SaveToInterfaceContext(InterfaceWrapContext interfaceWrapContext)
        {
            interfaceWrapContext.ContentType = HttpContentTypes.json;
        }
        internal override void SaveToMethodContext(MethodWrapContext methodWrapContext)
        {
            methodWrapContext.ContentType = HttpContentTypes.json;
        }
    }
}