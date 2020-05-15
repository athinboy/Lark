using Feign.Core.Attributes.RequestService;
using Feign.Core.Cache;
using Feign.Core.Context;
using Feign.Core.Serialize;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Feign.Core.Attributes
{
    /// <summary>
    /// json serialize .
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.ReturnValue, AllowMultiple = false, Inherited = true)]
    public class JsonAttribute : BaseAttribute
    {
        internal override void SaveToParameterContext(ParameterWrapContext parameterItem)
        {
            base.SaveToParameterContext(parameterItem);
            parameterItem.serializeType = SerializeTypes.json;
        }


    }
}
