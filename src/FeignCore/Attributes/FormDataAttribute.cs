using System;
using Feign.Core.Attributes;
using Feign.Core.Context;
using Feign.Core.Enum;

namespace FeignCore.Attributes
{
    /// <summary>
    /// form data serialize .
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class FormDataAttribute : BaseAttribute
    {
        internal override void SaveToInterfaceContext(InterfaceWrapContext interfaceWrapContext)
        {
            interfaceWrapContext.ContentType=HttpContentTypes.formdata; 
        }
    }
}