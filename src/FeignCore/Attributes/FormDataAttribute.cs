using System;
using Feign.Core.Attributes;
using Feign.Core.Context;
using Feign.Core.Serialize;

namespace FeignCore.Attributes
{
    /// <summary>
    /// form data serialize .
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter , AllowMultiple = false, Inherited = true)]
    public class FormDataAttribute : BaseAttribute
    {
        internal override void SaveToParameterContext(ParameterWrapContext parameterItem)
        {

            parameterItem.SerializeType = SerializeTypes.formdata;
        }
    }
}