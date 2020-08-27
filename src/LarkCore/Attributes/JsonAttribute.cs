using Lark.Core.Attributes.RequestService;
using Lark.Core.Cache;
using Lark.Core.Context;
using Lark.Core.Enum;
using Lark.Core.Attributes;
using Lark.Core.Context;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Lark.Core.Attributes
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
