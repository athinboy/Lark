using System;
using System.Collections.Generic;
using System.Text;
using Feign.Core.Cache;
using Feign.Core.Context;

namespace Feign.Core.Attributes
{

    /// <summary>
    /// requset body
    /// serialize a para
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
    public class BodyAttribute : FeignAttribute
    {
        internal override void SaveToParameterContext(ParameterWrapContext parameterItem)
        {
            base.SaveToParameterContext(parameterItem);
            parameterItem.IsBody = true;
        }
    }
}
