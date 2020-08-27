using Lark.Core.Attributes.RequestService;
using Lark.Core.Cache;
using Lark.Core.Context;
using Lark.Core.Enum;
using Lark.Core.Attributes;
using Lark.Core.Context;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;

namespace Lark.Core.Attributes
{
    /// <summary>
    /// xml serialize.
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Method | AttributeTargets.Parameter | AttributeTargets.ReturnValue, AllowMultiple = false, Inherited = true)]
    public class XmlAttribute : BaseAttribute
    {
        internal override void SaveToParameterContext(ParameterWrapContext parameterWrapContext)
        {
            base.SaveToParameterContext(parameterWrapContext);
            parameterWrapContext.SerializeType = SerializeTypes.xml;
        }

        internal override void SaveToMethodContext(MethodWrapContext methodWrapContext)
        {
            base.SaveToMethodContext(methodWrapContext);
            methodWrapContext.SerializeType = SerializeTypes.xml;

        }

        internal override void SaveToInterfaceContext(InterfaceWrapContext interfaceWrapContext)
        {
            base.SaveToInterfaceContext(interfaceWrapContext);
            interfaceWrapContext.SerializeType = SerializeTypes.xml;
        }
        internal override void SaveToReturnContext(ReturnContext returnContext)
        {
            returnContext.SerializeType = SerializeTypes.xml;
        }


    }
}
