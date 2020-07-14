using Feign.Core.Attributes.RequestService;
using Feign.Core.Cache;
using Feign.Core.Context;
using Feign.Core.Enum;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;

namespace Feign.Core.Attributes
{
    /// <summary>
    /// xml serialize.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.ReturnValue, AllowMultiple = false, Inherited = true)]
    public class XmlAttribute : BaseAttribute
    {
        internal override void SaveToParameterContext(ParameterWrapContext parameterWrapContext)
        {
            base.SaveToParameterContext(parameterWrapContext);
            parameterWrapContext.SerializeType = SerializeTypes.xml;
        }

        internal override void SaveToMethodContext(MethodWrapContext methodWrapContext)
        {

        }

        internal override void SaveToInterfaceContext(InterfaceWrapContext interfaceWrapContext)
        {

        }
        internal override void SaveToReturnContext(ReturnContext returnContext)
        {
            returnContext.SerializeType = SerializeTypes.xml;
        }


    }
}
