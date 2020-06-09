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

            parameterItem.SerializeType = SerializeTypes.json;
        }


        internal override void SaveToMethodContext(MethodWrapContext methodWrapContext)
        {

        }

        internal override void SaveToInterfaceContext(InterfaceWrapContext interfaceWrapContext)
        {

        }
        internal override void SaveToReturnContext(ReturnContext returnContext)
        {
            returnContext.SerializeType = SerializeTypes.json;
        }


    }
}
