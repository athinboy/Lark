using System;
using Lark.Core.Context;
using Lark.Core.Enum;
using Lark.Core.Attributes;
using Lark.Core.Context;

namespace LarkCore.Attributes
{
    /// <summary>
    /// form data serialize .
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class FormBodyAttribute : BaseAttribute
    {
        internal override void SaveToInterfaceContext(InterfaceWrapContext interfaceWrapContext)
        {
            interfaceWrapContext.ContentType = HttpContentTypes.formdata;
        }
        internal override void SaveToMethodContext(MethodWrapContext methodWrapContext)
        {
            methodWrapContext.ContentType = HttpContentTypes.formdata;
        }
    }
}